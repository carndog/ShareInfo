using System.Collections.Generic;

namespace ExcelServices
{
    public interface IExcelLoader
    {
        IEnumerable<object> Read(ExcelMapping mapping, string filename);
    }
}