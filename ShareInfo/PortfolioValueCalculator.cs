using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using ShareInfo.DataExtraction;

namespace ShareInfo
{
    public class PortfolioValueCalculator
    {
        private readonly IHoldingsProvider _provider;
        private readonly IEnumerable<ShareExtract> _shareExtracts;
        private readonly ISymbolProvider _symbolsProvider;
        private readonly IValuationFilePath _valuationFilePath;

        public PortfolioValueCalculator(IHoldingsProvider provider, IEnumerable<ShareExtract> shareExtracts, ISymbolProvider symbolsProvider, IValuationFilePath valuationFilePath)
        {
            _provider = provider;
            _shareExtracts = shareExtracts;
            _symbolsProvider = symbolsProvider;
            _valuationFilePath = valuationFilePath;
        }

        public IEnumerable<ShareValue> GetValues()
        {
            IEnumerable<ShareValue> shareValues = _provider.GetHoldings(_symbolsProvider, _valuationFilePath)
                .Select(x =>
                {
                    ShareExtract matchingExtract = _shareExtracts.FirstOrDefault(extract => x.Symbol == extract.Symbol);
                    ShareValue shareValue = new ShareValue
                    {
                        Symbol = x.Symbol,
                        Value = (x.NumberPurchased * matchingExtract?.Price ?? 0) / 100
                    };

                    shareValue.DisplayValue = $"{shareValue.Value:C}";

                    return shareValue;
                });

            return shareValues;
        }
    }
}
