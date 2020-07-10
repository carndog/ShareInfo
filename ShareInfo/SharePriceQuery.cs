using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ShareInfo
{
    public static class SharePriceQuery
    {
        private const string Ftse100FeedUrl = @"https://spreadsheets.google.com/feeds/list/0AhySzEddwIC1dEtpWF9hQUhCWURZNEViUmpUeVgwdGc/1/public/basic?sq=symbol={0}";

        private const string GoogleQueryUrl = @"https://www.google.co.uk/search?q={0}+share+price";

        private const string LseSearchUrl = @"http://www.lse.co.uk/shareprice.asp?shareprice={0}";

        public static async Task<string> GetFtse100Data(string symbol)
        {
            return await GetPriceData(string.Format(Ftse100FeedUrl, symbol));
        }

        public static async Task<HtmlDocument> GetGoogleSearchData(string symbol)
        {
            HtmlDocument htmlDocument = await Task.Factory.StartNew(() =>
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(string.Format(GoogleQueryUrl, symbol));

                return document;
            });

            return htmlDocument;
        }

        public static async Task<string> GetLseData(string symbol)
        {
            return await GetPriceData(string.Format(LseSearchUrl, symbol));
        }

        private static async Task<string> GetPriceData(string url)
        {
            using (HttpClient client = new HttpClient())
            { 
                string data = await client.GetStringAsync(url);

                return data;
            }
        }
    }
}