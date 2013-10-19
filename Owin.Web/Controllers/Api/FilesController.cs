using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Owin.Domain;
using Owin.Domain.Entities;
using Owin.Web.Models.Api;
using ServiceStack.Text;

namespace Owin.Web.Controllers.Api
{
    public class FilesController : ApiController
    {
        private readonly ISourceReader _excelReader;

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
                var resp = new HttpResponseMessage(HttpStatusCode.OK);
                var file = provider.Contents.FirstOrDefault(x => !string.IsNullOrEmpty(x.Headers.ContentDisposition.FileName));
                if (file != null)
                {
                    var stream = file.ReadAsStreamAsync().Result;

                    var filename = SaveFile(stream);

                    var products = _excelReader.GetProductsFromStream(filename);

                    var productsWithChangedProperties = FindProductsWithChangedProperties(products);

                    resp.Content = new StringContent(productsWithChangedProperties.ToJson(), Encoding.UTF8, "application/json");
                    return resp;
                }

                return resp;
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
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
            return Enumerable.Empty<Product>();
        }
    }
}