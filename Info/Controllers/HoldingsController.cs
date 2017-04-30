using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ShareInfo;

namespace Info.Controllers
{
    public class HoldingsController : ApiController
    {
        // GET api/<controller>
        public async Task<IHttpActionResult> GetAsync()
        {
            string[] symbols = SymbolProvider.GetSymbols();

            ShareExtractorsDirector director = new ShareExtractorsDirector(symbols);

            IEnumerable<ShareExtract> shareExtracts = await director.GetExtracts();

            IEnumerable<SharePriceInfo> infos = shareExtracts.Select(x => new SharePriceInfo
            {
                Name = x.Name,
                Symbol = x.Symbol,
                ShareIndex = x.ShareIndex,
                Change = x.Change,
                ChangePercentage = x.ChangePercentage,
                Price = x.Price
            });

            return Ok(infos);
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(string key)
        {
            ShareExtractorsDirector director = new ShareExtractorsDirector(new[] { key });

            IEnumerable<ShareExtract> shareExtracts = await director.GetExtracts();

            IEnumerable<SharePriceInfo> infos = shareExtracts.Select(x => new SharePriceInfo
            {
                Name = x.Name,
                Symbol = x.Symbol,
                ShareIndex = x.ShareIndex,
                Change = x.Change,
                ChangePercentage = x.ChangePercentage,
                Price = x.Price
            });

            return Ok(infos.First());
        }
    }
}