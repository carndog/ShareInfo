using System;
using System.Collections.Generic;
using System.Linq;
using ShareInfo.DataExtraction;

namespace ShareInfo
{
    public class PortfolioValueCalculator
    {
        private readonly IHoldingsProvider _provider;
        private readonly IEnumerable<ShareExtract> _shareExtracts;
        private readonly ISymbolProvider _symbolsProvider;

        public PortfolioValueCalculator(IHoldingsProvider provider, IEnumerable<ShareExtract> shareExtracts, ISymbolProvider symbolsProvider)
        {
            _provider = provider;
            _shareExtracts = shareExtracts;
            _symbolsProvider = symbolsProvider;
        }

        public IEnumerable<ShareValue> GetValues()
        {
            IEnumerable<ShareValue> shareValues = _provider.GetHoldings(_symbolsProvider)
                .Select(x =>
                {
                    ShareExtract matchingExtract = _shareExtracts.FirstOrDefault(extract => x.Symbol == extract.Symbol);
                    ShareValue shareValue = new ShareValue
                    {
                        Symbol = x.Symbol,
                        Value = x.NumberPurchased * matchingExtract?.Price ?? 0
                    };

                    shareValue.DisplayValue = $"{shareValue.Value / 100:C}";

                    return shareValue;
                });

            return shareValues;
        }
    }
}
