using System.Net.Http;
using System.Threading.Tasks;

namespace ShareInfo
{
    public class SharePriceQuery
    {
        private string _queryUrl = @"https://spreadsheets.google.com/feeds/list/0AhySzEddwIC1dEtpWF9hQUhCWURZNEViUmpUeVgwdGc/1/public/basic?sq=symbol={0}";

        public async Task<string> GetPrice(string symbol)
        {
            using (HttpClient client = new HttpClient())
            {
                _queryUrl = string.Format(_queryUrl, symbol);

                string data = await client.GetStringAsync(_queryUrl);

                return data;
            }
        }
    }
}