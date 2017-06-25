using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShareInfo;
using ShareInfo.DataExtraction;
using System.IO;
using System.Reflection;
using DTO;

namespace SharesTests
{
    [TestClass]
    public class PortfolioValueCalculatorTests
    {
        [TestMethod]
        public void GivenValuationFile_WhenOneRecord_ThenPopulateShareValueListWithASingleEntry()
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            directoryName = directoryName?.Replace(@"bin\Debug", "Resources");

            Mock<IValuationFilePath> valuationFilePathMock = new Mock<IValuationFilePath>();
            valuationFilePathMock.Setup(x => x.Path).Returns(directoryName);

            PortfolioValueCalculator calculator = new PortfolioValueCalculator(new HoldingsProvider(), new[]
            {
                new ShareExtract
                {
                    Symbol = "AZN",
                    Price = 5401m
                }
            }, new SymbolProvider(), valuationFilePathMock.Object);

            IEnumerable<ShareValue> shareValues = calculator.GetValues().ToList();

            shareValues.Should().NotBeNullOrEmpty();
            shareValues.Count().Should().Be(1);
            shareValues.ElementAt(0).Symbol.Should().Be("AZN");
            shareValues.ElementAt(0).Value.Should().Be(2376.44m);
            shareValues.ElementAt(0).DisplayValue.Should().Be("£2,376.44");
        }
    }
}
