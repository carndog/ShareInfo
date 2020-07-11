using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DTO;

namespace Storage
{
    public class PriceRepository : IPriceRepository
    {
        public void Add(AssetPrice price)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string insertQuery = @"
                    INSERT INTO [dbo].[Prices]([AssetId], [Symbol], [Name], [Price], [OriginalPrice], [Exchange],
                    , [Exchange], [AssetType], [Change], [ChangePercentage], [Open], [High], [Low], [Volume]
                    , [TradingDay], [Date]) 
                    VALUES (@AssetId, @Symbol, @Name, @Price, @OriginalPrice, @Exchange, @AssetType, @Open,
                    @High, @Low, @Volume, @TradingDay, @CurrentDateTime, @Change, @ChangePercent)";

                int result = db.Execute(insertQuery, price);
            }
        }
    }
}
