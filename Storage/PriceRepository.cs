using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace Storage
{
    public class PriceRepository : RepositoryBase, IPriceRepository
    {
        public async Task<bool> Add(AssetPrice price)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string insertQuery = @"
                    INSERT INTO [dbo].[Prices]([AssetId], [Symbol], [Name], [Price], [OriginalPrice], [Exchange],
                    [Exchange], [AssetType], [Change], [ChangePercentage], [Open], [High], [Low], [Volume],
                    [TradingDay], [Date]) 
                    VALUES (@AssetId, @Symbol, @Name, @Price, @OriginalPrice, @Exchange, @AssetType, @Open,
                    @High, @Low, @Volume, @TradingDay, @Date, @Change, @ChangePercent)";

                int result = await db.ExecuteAsync(insertQuery, price);

                return result == 1;
            }
        }
    }
}
