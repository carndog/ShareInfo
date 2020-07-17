using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace Storage
{
    public class PriceRepository : RepositoryBase, IPriceRepository
    {
        public async Task<int> Add(AssetPrice price)
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

        public Task<AssetPrice> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}