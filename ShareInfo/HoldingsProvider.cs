using System;
using System.Collections.Generic;
using System.Globalization;

namespace ShareInfo
{
    public class HoldingsProvider : IHoldingsProvider
    {
        public IEnumerable<Holding> GetHoldings(ISymbolProvider provider)
        {
            //TODO: CHANGE THIS TO BE A LIST OF TRANSACTIONS AND THEN GROUPED BY SYMBOL, EACH SYMBOLS HAS ITS OWN TRANSACTION RECORDS
            return new[]
            {
                new Holding
                {
                    Symbol = "AZN", BookCost = Decimal.Parse("£2008.78", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 44
                },
                new Holding
                {
                    Symbol = "BA", BookCost = Decimal.Parse("£2026.72", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 379
                },
                new Holding
                {
                    Symbol = "BDEV", BookCost = Decimal.Parse("£2112.39", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 507
                },
                new Holding
                {
                    Symbol = "BP", BookCost = Decimal.Parse("£2097.04", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 466
                },
                new Holding
                {
                    Symbol = "BRBY", BookCost = Decimal.Parse("£2050.30", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 172
                },
                new Holding
                {
                    Symbol = "ESNT", BookCost = Decimal.Parse("£2085.18", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 433
                },
                new Holding
                {
                    Symbol = "ITV", BookCost = Decimal.Parse("£2026.33", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 1221
                },
                new Holding
                {
                    Symbol = "LLOY", BookCost = Decimal.Parse("£2030.35", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 3663
                },
                new Holding
                {
                    Symbol = "NTG", BookCost = Decimal.Parse("£2097.32", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 627
                },
                new Holding
                {
                    Symbol = "RMV", BookCost = Decimal.Parse("£1996.28", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 57
                },
                new Holding
                {
                    Symbol = "RR", BookCost = Decimal.Parse("£1992.99", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 280
                },
                new Holding
                {
                    Symbol = "RDSB", BookCost = Decimal.Parse("£2052.27", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 98
                },
                new Holding
                {
                    Symbol = "SL", BookCost = Decimal.Parse("£2043.26", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 705
                },
                new Holding
                {
                    Symbol = "TW", BookCost = Decimal.Parse("£2006.64", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 1536
                },
                new Holding
                {
                    Symbol = "TED", BookCost = Decimal.Parse("£1988.34", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint), NumberPurchased = 89
                }
            };
        }
    }
}