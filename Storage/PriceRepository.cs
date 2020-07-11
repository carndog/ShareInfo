using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DTO;

namespace Storage
{
    public class PriceRepository : IPriceRepository
    {
        private static readonly string ConnectionString = ConfigurationManager.AppSettings["connectionString"];

        public bool Add(AssetPrice price)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string insertQuery = @"
                    INSERT INTO [dbo].[Prices]([AssetId], [Symbol], [Name], [Price], [OriginalPrice], [Exchange],
                    , [Exchange], [AssetType], [Change], [ChangePercentage], [Open], [High], [Low], [Volume]
                    , [TradingDay], [Date]) 
                    VALUES (@AssetId, @Symbol, @Name, @Price, @OriginalPrice, @Exchange, @AssetType, @Open,
                    @High, @Low, @Volume, @TradingDay, @CurrentDateTime, @Change, @ChangePercent)";

                int result = db.Execute(insertQuery, price);

                return result == 1;
            }
        }
    }
}
