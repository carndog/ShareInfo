using System;
using System.Web.UI;
using Shares;

namespace SharePriceList
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            SharePriceQuery query = new SharePriceQuery();

            string price = await query.GetPrice(txtSymbol.Text);

            lblPrices.Text = price;
        }
    }
}