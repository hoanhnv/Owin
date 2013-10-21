using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Owin.Data;
using Owin.Domain;
using Owin.Domain.Entities;

namespace Owin.Web.Controllers.Api
{
    public class FilesController : ApiController
    {
        private readonly ISourceReader _excelReader;
        private HttpClient _httpClient;
        public FilesController()
        {
            _excelReader = new ExcelReader();
        }

        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "please submit a valid request");
            }
            var provider = new MultipartMemoryStreamProvider();
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var file = provider.Contents.FirstOrDefault(x => !string.IsNullOrEmpty(x.Headers.ContentDisposition.FileName));
                if (file != null)
                {
                    InitHttpClient();
                    var stream = file.ReadAsStreamAsync().Result;
                    var filename = SaveFile(stream);
                    var products = _excelReader.GetProductsFromFile(filename);
                    var productsWithChangedProperties = FindProductsWithChangedProperties(products);

                    return Request.CreateResponse(HttpStatusCode.OK, productsWithChangedProperties, "application/json");
                }

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Неверный файл");
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        private void InitHttpClient()
        {
            _httpClient=new HttpClient {BaseAddress = new Uri("http://www.cameronsino.net")};
            var content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("Username", "slavakis"),
                new KeyValuePair<string, string>("Password", "131313"),
                new KeyValuePair<string, string>("text", "9384"),
                new KeyValuePair<string, string>("checkCode", "9384")
            });
            var result = _httpClient.PostAsync("/", content).Result;
        }

        private string SaveFile(Stream stream)
        {
            var filename = Path.GetTempFileName();
            using (var tmpFile = File.OpenWrite(filename))
            {
                stream.CopyTo(tmpFile);
            }
            return filename;

        }

        private IEnumerable<Product> FindProductsWithChangedProperties(IEnumerable<Product> products)
        {
            IList<Product> productsInDatabase;
            using (var context = new DatabaseContext("DatabaseContext"))
            {
                productsInDatabase = context.Products.AsNoTracking().ToList();
            }

            var taskList = new List<Task<Product>>();

            foreach (var task in products
                .Select(product => new Task<Product>(o => ProductTask((Product) o, productsInDatabase)
                                                     , product)))
            {
                task.Start();
                taskList.Add(task);
            }
            var taskArray = taskList.ToArray();
            Task.WaitAll(taskArray);
            return taskArray.Select(task => task.Result).Where(result => result != null);
        }

        private Product ProductTask(Product product, IEnumerable<Product> products)
        {
            var productInDatabase = products.FirstOrDefault(x => x.Code == product.Code);
            if (productInDatabase == null) return product;
            if (HasDifferences(product, productInDatabase))
            {
                product.Id = productInDatabase.Id;
                product.OldPrice = productInDatabase.Price;
                return product;
            }
            return null;
        }


        private static bool HasDifferences(Product product, Product productInDatabase)
        {
            return true; // product.Price != productInDatabase.Price;
        }
    }
}