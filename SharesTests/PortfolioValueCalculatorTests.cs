using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShareInfo;
using ShareInfo.DataExtraction;

namespace SharesTests
{
    [TestClass]
    public class PortfolioValueCalculatorTests
    {
        [TestMethod]
        public void GetSymbols()
        {
            Mock<IHoldingsProvider> holdingsProvider = new Mock<IHoldingsProvider>();
            holdingsProvider.Setup(x => x.GetHoldings(It.IsAny<ISymbolProvider>()))
                .Returns(new[]
                {
                    new Holding
                    {
                        NumberPurchased = 200,
                        Symbol = "AZN"
                    },
                    new Holding
                    {
                        NumberPurchased = 200,
                        Symbol = "BRBY"
                    }
                });

            PortfolioValueCalculator calculator = new PortfolioValueCalculator(holdingsProvider.Object, new[]
            {
                new ShareExtract
                {
                    Symbol = "AZN",
                    Price = 5160m
                },
                new ShareExtract
                {
                    Symbol = "BRBY",
                    Price = 23.56m
                }
            }, new SymbolProvider());

            IEnumerable<ShareValue> shareValues = calculator.GetValues().ToList();

            shareValues.Should().NotBeNullOrEmpty();
            shareValues.Count().Should().Be(2);
            shareValues.ElementAt(0).Symbol.Should().Be("AZN");
            shareValues.ElementAt(0).Value.Should().Be(1032000);
            shareValues.ElementAt(1).Symbol.Should().Be("BRBY");
            shareValues.ElementAt(1).Value.Should().Be(4712);
        }
    }
}
