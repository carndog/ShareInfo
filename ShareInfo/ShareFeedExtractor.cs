using System.Xml;

namespace ShareInfo
{
    public class ShareFeedExtractor
    {
        private ShareFeedExtractor() { }

        public static ShareFeedExtractor Create(string data)
        {
            XmlDocument document = new XmlDocument();
            document.Load(data);

            return new ShareFeedExtractor();
        }
    }
}
