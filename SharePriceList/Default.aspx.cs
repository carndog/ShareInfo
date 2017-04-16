using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShareInfo;

namespace SharePriceList
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private async Task LoadData()
        {
            rptHoldings.DataMember = "ShareFeedExtract";
            IEnumerable<string> symbols = SymbolProvider.GetSymbols();
            var director = new ShareExtractorsDirector(symbols);
            rptHoldings.DataSource = await director.GetExtracts();
            rptHoldings.DataBind();
        }

        protected async void btnSubmit_OnClick(object sender, EventArgs e)
        {
            //#fac-ut > div.vk_c.card-section.fac-lstng

            await LoadData();
        }
    }
}