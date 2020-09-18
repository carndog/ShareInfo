using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage
{
    public class PriceRepository : Database, IPriceRepository
    {
        public async Task<int> AddAsync(AssetPrice price)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string insertQuery = @"
                    DECLARE @InsertedRows AS TABLE (Id int);

                    INSERT INTO [dbo].[Prices] 
                    (
                        [AssetId], [Symbol], [Name], [Price], [OriginalPrice], [Exchange], [AssetType], [Open], 
                        [High], [Low], [Volume], [TradingDay], [CurrentDateTime], [TimeZone], [Change], [ChangePercentage]
                    ) 
                    OUTPUT INSERTED.Id INTO @InsertedRows
                    VALUES 
                    (
                        @AssetId, @Symbol, @Name, @Price, @OriginalPrice, @Exchange, @AssetType, @Open,
                        @High, @Low, @Volume, @TradingDay, @CurrentDateTime, @TimeZone, @Change, @ChangePercentage
                    );

                    SELECT Id FROM @InsertedRows
                ";

                IEnumerable<int> results = await db.QueryAsync<int>(insertQuery,
                    new
                    {
                        price.AssetId,
                        price.Symbol,
                        price.Name,
                        price.Price,
                        price.OriginalPrice,
                        price.Exchange,
                        price.AssetType,
                        price.Open,
                        price.High,
                        price.Low,
                        price.Volume,
                        price.TradingDay,
                        price.CurrentDateTime,
                        price.TimeZone,
                        price.Change,
                        price.ChangePercentage,
                    });
                int id = results.Single();

                return id;
            }
        }

        public async Task<AssetPrice> GetAsync(int id)
        {
            AssetPrice price;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                        [Id],                         
                        [AssetId], 
                        [Symbol], 
                        [Name], 
                        [Price], 
                        [OriginalPrice], 
                        [Exchange], 
                        [AssetType], 
                        [Open], 
                        [High], 
                        [Low], 
                        [Volume], 
                        [TradingDay], 
                        [CurrentDateTime], 
                        [TimeZone], 
                        [Change], 
                        [ChangePercentage]
                      FROM [dbo].[Prices]  WHERE Id = @Id";

                price = await connection.QuerySingleAsync<AssetPrice>(query, new {Id = id});
            }

            return price;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<object> records = await connection.QueryAsync<object>(
                    "SELECT 1 WHERE EXISTS (SELECT 1 FROM [dbo].[Prices] WHERE ID = @id)", new {id});
                return records.Any();
            }
        }
    }
}