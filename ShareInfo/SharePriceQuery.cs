using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ShareInfo
{
    public static class SharePriceQuery
    {
        private const string QueryUrl = @"https://spreadsheets.google.com/feeds/list/0AhySzEddwIC1dEtpWF9hQUhCWURZNEViUmpUeVgwdGc/1/public/basic?sq=symbol={0}";

        private const string GoogleQuery = @"https://www.google.co.uk/search?q={0}+share+price";

        public static async Task<string> GetFtse100Data(string symbol)
        {
            return await GetPriceData(string.Format(QueryUrl, symbol));
        }

        public static async Task<HtmlDocument> GetGoogleSearchData(string symbol)
        {
            HtmlDocument htmlDocument = await Task.Factory.StartNew(() =>
            {
                var web = new HtmlWeb();
                HtmlDocument document = web.Load(string.Format(GoogleQuery, symbol));

                return document;
            });

            return htmlDocument;
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