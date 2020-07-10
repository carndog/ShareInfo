using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DTO;
using ShareInfo;
using ShareInfo.DataExtraction;

namespace Info.Controllers
{
    public class HoldingsController : ApiController
    {
        public async Task<IHttpActionResult> GetAsync()
        {
            var symbolProvider = new SymbolProvider();
            string[] symbols = symbolProvider.GetSymbols();

            ShareExtractorsDirector director = new ShareExtractorsDirector(symbols);

            IEnumerable<AssetPrice> shareExtracts = await director.GetExtracts();

            if (shareExtracts == null)
            {
                throw new InvalidOperationException();
            }

            IEnumerable<AssetPrice> extracts = shareExtracts as AssetPrice[] ?? shareExtracts.ToArray();

            IHoldingsProvider holdingsProvider = new HoldingsProvider();

            IValuationFilePath valuationFilePath = new ValuationFilePath();

            PortfolioValueCalculator calculator = new PortfolioValueCalculator(holdingsProvider, extracts, symbolProvider, valuationFilePath);

            IEnumerable<ShareValue> shareValues = calculator.GetValues();

            IEnumerable<Holding> holdings = holdingsProvider.GetHoldings(symbolProvider, valuationFilePath);

            IEnumerable<SharePrice> infos = extracts.Select(x =>
            {
                ShareValue shareValue = shareValues.First(value => value.Symbol == x.Symbol);

                Holding holdingInfo = holdings.First(holding => holding.Symbol == x.Symbol);

                SharePrice sharePrice = new SharePrice
                {
                    Name = x.Name,
                    Symbol = x.Symbol,
                    ShareIndex = x.ShareIndex,
                    Change = x.Change,
                    ChangePercentage = x.ChangePercentage,
                    Price = x.Price,
                    NumberHeld = holdingInfo.NumberPurchased,
                    Value = shareValue.Value,
                    DisplayValue = shareValue.DisplayValue
                };

                return sharePrice;
            }).OrderByDescending(x => x.Value).ToArray();

            InformationBoard board = new InformationBoard
            {
                SharePrices = infos,
                Total = new Total { Value = infos.Sum(x => x.Value) }
            };

            return Ok(board);
        }

        public async Task<IHttpActionResult> Get(string key)
        {
            ShareExtractorsDirector director = new ShareExtractorsDirector(new[] { key });

            IEnumerable<AssetPrice> shareExtracts = await director.GetExtracts();

            IEnumerable<SharePrice> infos = shareExtracts.Select(x => new SharePrice
            {
                Name = x.Name,
                Symbol = x.Symbol,
                ShareIndex = x.ShareIndex,
                Change = x.Change,
                ChangePercentage = x.ChangePercentage,
                Price = x.Price
            });

            return Ok(infos.First());
        }
    }
}