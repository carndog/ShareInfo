namespace ShareInfo
{
    public class SymbolProvider : ISymbolProvider
    {
        public string[] GetSymbols()
        {
            string[] symbols = { "ESNT", "RDSB", "TED", "TW", "SL", "BA", "BP", "NTG", "BRBY", "BDEV", "RR", "LLOY", "ITV", "AZN", "RMV" };

            return symbols;
        }
    }
}
