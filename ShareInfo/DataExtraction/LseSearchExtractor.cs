using System;
using System.Threading.Tasks;

namespace ShareInfo.DataExtraction
{
    public class LseSearchExtractor : ShareDataExtractor
    {
        public override async Task<ShareExtract> GetExtract(string symbol)
        {
            string rawData = await SharePriceQuery.GetLseData(symbol);

            ShareExtract extract = Create(rawData);

            if (string.IsNullOrWhiteSpace(extract.Name))
            {
                return await Successor.GetExtract(symbol);
            }

            extract.Symbol = symbol;

            return extract;
        }

        private ShareExtract Create(string rawData)
        {
            ShareExtract extract = new ShareExtract();

            const string sharePrice = "Share Price:";
            const string name = "Share Price Information for";

            int firstPositionPrice = rawData.IndexOf(sharePrice, StringComparison.OrdinalIgnoreCase);
            int firstPositionName = rawData.IndexOf(name, StringComparison.OrdinalIgnoreCase);

            if (firstPositionName == -1 || firstPositionPrice == -1)
            {
                return extract;
            }

            int lastPositionName = rawData.IndexOf("(", firstPositionName, StringComparison.Ordinal);
            extract.Name = rawData.Substring(firstPositionName + name.Length, lastPositionName - firstPositionName - name.Length).Trim();

            bool dataCollected = false;
            int current = firstPositionPrice + sharePrice.Length;

            while (!dataCollected)
            {
                while (!char.IsDigit(rawData[++current]))
                {
                }

                int lastPosition = rawData.IndexOf("<", current, StringComparison.Ordinal);
                string price = rawData.Substring(current, lastPosition - current);
                extract.Price = Convert.ToDecimal(price);
                dataCollected = true;
            }

            return extract;
        }
    }
}