Create NonClustered Index IX_PeriodPrice On PeriodPrice
    (Symbol Asc, [Date] Asc)

Create NonClustered Index IX_Price On Prices
    (Symbol Asc, Exchange Asc, CurrentDateTime Asc)