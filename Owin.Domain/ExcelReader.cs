using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinqToExcel;
using LinqToExcel.Query;
using Owin.Domain.Entities;
using Remotion.Data.Linq.Parsing.Structure.IntermediateModel;

namespace Owin.Domain
{
    public class ExcelReader : ISourceReader
    {
        public IEnumerable<Product> GetProductsFromFile(string filename)
        {
            var excel = new ExcelQueryFactory(filename);
            var worksheetNames = excel.GetWorksheetNames().ToArray();
            //
            //Mapping
            //
            for (int i = 0; i < worksheetNames.Length; i++)
            {
                var columnNames = excel.GetColumnNames(worksheetNames[i]);
                var titleRow = FindTitleRow(excel.WorksheetNoHeader(worksheetNames[i]));

                var codeIndex = 0;
                var priceIndex = GetColumnIndex(titleRow, "Price");

                bool skip = true;
                foreach (var product in excel.WorksheetNoHeader(worksheetNames[i])
                    .Where(x => x[codeIndex] != ""))
                {
                    if (skip)
                    {
                        skip = product[codeIndex].ToString().Trim() != "No.:";
                        continue;
                    }
                    yield return 
                        new Product
                        {
                            Code = product[codeIndex].ToString().Trim(),
                            Price = product[priceIndex].Cast<decimal>()
                        };
                }
            }
        }

        private int GetColumnIndex(List<Cell> titleRow, string title)
        {
            return titleRow.FindIndex(c => c.ToString() == title);
        }

        private RowNoHeader FindTitleRow(IQueryable<RowNoHeader> rows)
        {
            return rows.FirstOrDefault(row => row[0] == "Item");
        }
    }

    public interface ISourceReader
    {
        IEnumerable<Product> GetProductsFromFile(string filename);
    }
}
