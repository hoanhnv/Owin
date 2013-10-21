using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should.Fluent;

namespace Owin.Domain.Tests
{
    [TestClass]
    public class LinqToExcelTests
    {
        private const string excelFileName = @"files\price.xls";

        [TestMethod]
        public void InitFactoryTest()
        {
            var reader = new ExcelReader();
            var result = reader.GetProductsFromFile(excelFileName);


            var resultArray = result.ToArray();
            resultArray.Should().Not.Be.Null()
                  .Should().Count.AtLeast(1);
        }
    }
}
