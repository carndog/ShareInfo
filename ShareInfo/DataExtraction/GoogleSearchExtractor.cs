using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DTO;
using HtmlAgilityPack;

namespace ShareInfo.DataExtraction
{
    public class GoogleSearchExtractor : ShareDataExtractor
    {
        public static readonly Regex ShareResultRegex = new Regex("^.*-.*\\d", RegexOptions.Compiled);

        public override async Task<AssetPrice> GetExtract(string symbol)
        {
            HtmlDocument rawData = await SharePriceQuery.GetGoogleSearchData(symbol);

            AssetPrice extract = Create(rawData);

            if (string.IsNullOrWhiteSpace(extract.Name))
            {
                return new AssetPrice { Symbol = symbol };
            }

            return extract;
        }

        private AssetPrice Create(HtmlDocument document)
        {
            string rawData = document.GetElementbyId("search").InnerText;

            string searchResultValue = ShareResultRegex.Match(rawData).Value;
            string[] strings = Regex.Split(searchResultValue, @"\(LON\)");

            AssetPrice assetPrice = new AssetPrice();

            try
            {
                assetPrice.Symbol = strings[0].Split("-".ToCharArray())[0].Trim();
                assetPrice.Name = strings[0].Split("-".ToCharArray())[1].Trim();

                decimal priceValue = Convert.ToDecimal(strings[1].Split("@".ToCharArray())[0].Split("&".ToCharArray())[0]);
                assetPrice.Price = Math.Round(priceValue, 2);

                assetPrice.Change = Convert.ToDecimal(
                    strings[1].Split("@".ToCharArray())[0].Split("&".ToCharArray())[1].Split("+".ToCharArray())[1]
                        .Split("(".ToCharArray())[0]
                        .Trim());

                assetPrice.ChangePercentage =
                    Math.Round(Convert.ToDecimal(
                        strings[1].Split("@".ToCharArray())[0].Split("&".ToCharArray())[1].Split("+".ToCharArray())[1]
                            .Split("(".ToCharArray())[1]
                            .Split("%".ToCharArray())[0]), 2);
            }
            catch
            {
                
            }

            return assetPrice;
        }
    }
}