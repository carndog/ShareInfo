﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage
{
    public class PriceRepository : Repository, IPriceRepository
    {
        private readonly IGetDatabase _getDatabase;

        public PriceRepository(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }

        public async Task<int> AddAsync(AssetPrice price)
        {
            using (IDbConnection db = new SqlConnection(_getDatabase.GetConnectionString()))
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
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
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
            return await base.ExistsAsync(id, "Prices");
        }

        public async Task<int> CountAsync()
        {
            return await base.CountAsync("Prices");
        }
        
        public override string GetConnectionString() => _getDatabase.GetConnectionString();
    }
}