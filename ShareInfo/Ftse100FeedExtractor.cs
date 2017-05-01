using System;
using System.Threading.Tasks;
using System.Xml;

namespace ShareInfo
{
    public class Ftse100FeedExtractor : ShareDataExtractor
    {
        public override async Task<ShareExtract> GetExtract(string symbol)
        {
            string rawData = await SharePriceQuery.GetFtse100Data(symbol + ".L");

            ShareExtract extract = Create(rawData);

            if (string.IsNullOrWhiteSpace(extract.Name))
            {
                return await Successor.GetExtract(symbol);
            }

            return extract;
        }

        private ShareExtract Create(string rawData)
        {
            ShareExtract extract = new ShareExtract();

            XmlDocument document = new XmlDocument();
            document.LoadXml(rawData);

            XmlNodeList titles = document.GetElementsByTagName("title");

            //TODO: xpath not working

            extract.ShareIndex = titles?.Item(0)?.InnerText;
            extract.Symbol = titles?.Item(1)?.InnerText.Split('.')[0];

            string[] content = document.GetElementsByTagName("content")?.Item(0)?.InnerText?.Split(',');
            if (content != null && content.Length == 3)
            {
                extract.Name = content[0].Split(':')[1].Trim();
                decimal priceValue = Convert.ToDecimal(content[1].Split(':')[1].Trim());
                extract.Price = Math.Round(priceValue, 2);
                extract.Change = Convert.ToDecimal(content[2].Split(':')[1].Trim());
                decimal? changePercentageValue = Math.Abs(extract.Change.Value) / extract.Price * 100;
                extract.ChangePercentage = Math.Round(changePercentageValue.Value, 2);
            }

            return extract;
        }
    }
}