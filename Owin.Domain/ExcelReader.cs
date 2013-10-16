using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin.Domain.Entities;

namespace Owin.Domain
{
    public class ExcelReader:ISourceReader
    {
        public IEnumerable<Product> GetProductsFromStream(Stream stream)
        {
            return Enumerable.Empty<Product>();
        }
    }

    public interface ISourceReader
    {
        IEnumerable<Product> GetProductsFromStream(Stream stream);
    }
}
