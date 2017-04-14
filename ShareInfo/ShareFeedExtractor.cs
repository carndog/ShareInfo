using System;
using System.Xml;

namespace ShareInfo
{
    public class ShareFeedExtractor
    {
        private ShareFeedExtractor() { }

        public string ShareIndex { get; private set; }

        public string Symbol { get; private set; }

        public string Name { get; private set; }

        public decimal? Price { get; private set; }

        public decimal? Change { get; private set; }

        public decimal? ChangePercentage { get; private set; }

        public static ShareFeedExtractor Create(string data)
        {
            ShareFeedExtractor extractor = new ShareFeedExtractor();

            XmlDocument document = new XmlDocument();
            document.LoadXml(data);

            XmlNodeList titles = document.GetElementsByTagName("title");

            //TODO: xpath not working

            extractor.ShareIndex = titles?.Item(0)?.InnerText;
            extractor.Symbol = titles?.Item(1)?.InnerText.Split('.')[0];

            string[] content = document.GetElementsByTagName("content")?.Item(0)?.InnerText?.Split(',');
            if (content != null && content.Length == 3)
            {
                extractor.Name = content[0].Split(':')[1].Trim();
                extractor.Price = Convert.ToDecimal(content[1].Split(':')[1].Trim());
                extractor.Change = Convert.ToDecimal(content[2].Split(':')[1].Trim());
                extractor.ChangePercentage = extractor.Change / extractor.Price * 100;
            }

            return extractor;
        }
    }
}
