using System;
using System.Collections.Generic;

namespace ExcelServices
{
    public class ExcelMapping
    {
        public ExcelMapping()
        {
        }
        
        public int SheetIndex { get; set; }
        
        public int ExpectedHeaderRowIndex { get; set; }

        public IDictionary<int, string> ExpectedColumnHeaders { get; set; }
        
        public Type TargetType { get; set; }
        
        public IDictionary<int, string> TargetProperties { get; set; }
    }
}