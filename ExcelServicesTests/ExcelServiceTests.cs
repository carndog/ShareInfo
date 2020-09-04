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
        public void Should_ReadContents()
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
                    {10, "CloseDate"},
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
            
            IEnumerable<object> objects = _excelLoader.Read(excelMapping);
            
            Assert.That(objects.Count(), Is.EqualTo(1));
        }
    }
}