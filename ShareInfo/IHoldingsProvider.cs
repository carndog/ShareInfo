using System.Collections.Generic;

namespace ShareInfo
{
    public interface IHoldingsProvider
    {
        IEnumerable<Holding> GetHoldings(ISymbolProvider provider);
    }
}