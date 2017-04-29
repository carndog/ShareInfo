using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Microsoft.Data.OData;
using ShareInfo;

namespace SharePriceListWeb.Controllers
{
    public class SharePriceInfoController : ODataController
    {
        private static readonly ODataValidationSettings ValidationSettings = new ODataValidationSettings();

        // GET: odata/SharePriceInfo
        public async Task<IHttpActionResult> GetSharePriceInfo(ODataQueryOptions<SharePriceInfo> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(ValidationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

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

            return Ok(infos.ToList());
        }

        // GET: odata/SharePriceInfo(5)
        public async Task<IHttpActionResult> GetSharePriceInfo([FromODataUri] string key, ODataQueryOptions<SharePriceInfo> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(ValidationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

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

            return Ok<SharePriceInfo>(infos.First());
        }
    }
}
