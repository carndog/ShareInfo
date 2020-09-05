using System.Collections.Generic;
using System.Linq;
using DTO;
using ExcelServices;
using NUnit.Framework;

namespace ServicesTests
{
    public class ExcelServiceTests
    {
        private ExcelLoader _excelLoader;

        [SetUp]
        public void Setup()
        {
            _excelLoader = new ExcelLoader();
        }

        [Test]
        public void Should_ReadContents_WhenClosedPosition()
        {
            ExcelMapping excelMapping = new ExcelMapping
            {
                SheetIndex = 1,
                TargetProperties = new Dictionary<int, string>
                {
                    {0, "PositionId"},
                    {1, "Action"},
                    {3, "Amount"},
                    {4, "Units"},
                    {5, "OpenRate"},
                    {6, "CloseRate"},
                    {7, "Spread"},
                    {8, "Profit"},
                    {9, "OpenDate"},
                    {10, "ClosedDate"},
                    {11, "TakeProfitRate"},
                    {12, "StopLossRate"},
                    {13, "RollOverFees"},
                },
                ExpectedColumnHeaders = new Dictionary<int, string>
                {
                    {0, "Position Id"},
                    {1, "Action"},
                    {3, "Amount"},
                    {4, "Units"},
                    {5, "Open Rate"},
                    {6, "Close Rate"},
                    {7, "Spread"},
                    {8, "Profit"},
                    {9, "Open Date"},
                    {10, "Close Date"},
                    {11, "Take Profit Rate"},
                    {12, "Stop Loss Rate"},
                    {13, "RollOver Fees And Dividends"},
                },
                TargetType = typeof(EtoroClosedPosition)
            };
            
            IEnumerable<object> objects = _excelLoader.Read(excelMapping, "file.xlsx");
            
            Assert.That(objects.Count(), Is.EqualTo(1));
        }
        
        [Test]
        public void Should_ReadContents_WhenTransaction()
        {
            ExcelMapping excelMapping = new ExcelMapping
            {
                SheetIndex = 2,
                TargetProperties = new Dictionary<int, string>
                {
                    {0, "Date"},
                    {1, "AccountBalance"},
                    {2, "Type"},
                    {3, "Details"},
                    {4, "PositionId"},
                    {5, "Amount"},
                    {6, "RealizedEquityChange"},
                    {7, "RealizedEquity"}
                },
                ExpectedColumnHeaders = new Dictionary<int, string>
                {
                    {0, "Date"},
                    {1, "Account Balance"},
                    {2, "Type"},
                    {3, "Details"},
                    {4, "Position Id"},
                    {5, "Amount"},
                    {6, "Realized Equity Change"},
                    {7, "Realized Equity"}
                },
                TargetType = typeof(EtoroTransaction)
            };
            
            IEnumerable<object> objects = _excelLoader.Read(excelMapping, "file.xlsx");
            
            Assert.That(objects.Count(), Is.EqualTo(23));
        }
        
        [Test]
        public void Should_ReadContents_WhenHalifaxDividends()
        {
            ExcelMapping excelMapping = new ExcelMapping
            {
                SheetIndex = 0,
                TargetProperties = new Dictionary<int, string>
                {
                    {0, "IssueDate"},
                    {1, "Stock"},
                    {2, "ExDividendDate"},
                    {3, "SharesHeld"},
                    {4, "Amount"},
                    {5, "HandlingOption"}
                },
                ExpectedColumnHeaders = new Dictionary<int, string>
                {
                    {0, "Issue Date"},
                    {1, "Stock"},
                    {2, "XD Date"},
                    {3, "Shares held on XD Date"},
                    {4, "Amount Payable"},
                    {5, "Handling Option"}
                },
                TargetType = typeof(HalifaxDividend)
            };
            
            IEnumerable<object> objects = _excelLoader.Read(excelMapping, "halifax.xlsx");
            
            Assert.That(objects.Count(), Is.EqualTo(796));
        }
    }
}