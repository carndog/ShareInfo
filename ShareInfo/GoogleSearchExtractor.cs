using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ShareInfo
{
    public class GoogleSearchExtractor : ShareDataExtractor
    {
        public static readonly Regex ShareResultRegex = new Regex("^.*-.*\\d", RegexOptions.Compiled);

        public override async Task<ShareExtract> GetExtract(string symbol)
        {
            HtmlDocument rawData = await SharePriceQuery.GetGoogleSearchData(symbol);

            ShareExtract extract = Create(rawData);

            if (string.IsNullOrWhiteSpace(extract.Name))
            {
                return new ShareExtract { Symbol = symbol };
            }

            return extract;
        }

        private ShareExtract Create(HtmlDocument document)
        {
            string rawData = document.GetElementbyId("search").InnerText;

            string searchResultValue = ShareResultRegex.Match(rawData).Value;
            string[] strings = Regex.Split(searchResultValue, @"\(LON\)");

            var shareExtract = new ShareExtract();

            try
            {
                shareExtract.Symbol = strings[0].Split("-".ToCharArray())[0].Trim();
                shareExtract.Name = strings[0].Split("-".ToCharArray())[1].Trim();

                shareExtract.Price =
                    Convert.ToDecimal(strings[1].Split("@".ToCharArray())[0].Split("&".ToCharArray())[0]);

                shareExtract.Change = Convert.ToDecimal(
                    strings[1].Split("@".ToCharArray())[0].Split("&".ToCharArray())[1].Split("+".ToCharArray())[1]
                        .Split("(".ToCharArray())[0]
                        .Trim());

                shareExtract.ChangePercentage =
                    Math.Round(Convert.ToDecimal(
                        strings[1].Split("@".ToCharArray())[0].Split("&".ToCharArray())[1].Split("+".ToCharArray())[1]
                            .Split("(".ToCharArray())[1]
                            .Split("%".ToCharArray())[0]), 2);
            }
            catch
            {
                
            }

            return shareExtract;
        }
    }
}