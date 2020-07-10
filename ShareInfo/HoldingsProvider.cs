using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace ShareInfo
{
    public class HoldingsProvider : IHoldingsProvider
    {
        private const int Company = 0;
        private const int NumberPurchased = 1;
        private const int BookCost = 3;

        public IEnumerable<Holding> GetHoldings(ISymbolProvider provider, IValuationFilePath valuationFilePath)
        {
            List<Holding> results = new List<Holding>(20);

            DirectoryInfo directory = new DirectoryInfo(valuationFilePath.Path);
            FileInfo[] fileInfos = directory.GetFiles("valuation*.csv");
            string fullPath = fileInfos.FirstOrDefault()?.FullName;

            if (fullPath != null)
            {
                using (TextFieldParser parser = new TextFieldParser(fullPath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    parser.ReadLine();

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        Holding holding = new Holding();

                        if (!string.IsNullOrWhiteSpace(fields?[0]))
                        {
                            holding.Symbol = fields[Company].TrimEnd('.');
                            holding.NumberPurchased = Convert.ToInt32(fields[NumberPurchased]);
                            holding.BookCost = Convert.ToDecimal(fields[BookCost].Substring(1));
                            results.Add(holding);
                        }
                    }
                }
            }

            return results;
        }
    }
}