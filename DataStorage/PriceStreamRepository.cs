using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;
using NodaTime;

namespace DataStorage
{
    public class PriceStreamRepository : Repository, IPriceStreamRepository
    {
        private readonly IGetDatabase _getDatabase;

        public PriceStreamRepository(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }

        public async Task<int> AddAsync(PriceStream priceStream)
        {
            using (IDbConnection db = new SqlConnection(_getDatabase.GetConnectionString()))
            {
                string insertQuery = @"
                    DECLARE @InsertedRows AS TABLE (Id int);

                    INSERT INTO [dbo].[PriceStream] 
                    (
                        [PriceStreamId], [Exchange], [Symbol], [Price], [OriginalPrice], [CurrentDateTime], [TimeZone]
                    ) 
                    OUTPUT INSERTED.Id INTO @InsertedRows
                    VALUES 
                    (
                        @PriceStreamId, @Exchange, @Symbol, @Price, @OriginalPrice, @CurrentDateTime, @TimeZone
                    );

                    SELECT Id FROM @InsertedRows
                ";

                IEnumerable<int> results = await db.QueryAsync<int>(insertQuery,
                    new
                    {
                        priceStream.PriceStreamId,
                        priceStream.Exchange,
                        priceStream.Symbol,
                        priceStream.Price,
                        priceStream.OriginalPrice,
                        priceStream.CurrentDateTime,
                        priceStream.TimeZone
                    });
                int id = results.Single();

                return id;
            }
        }

        public async Task<PriceStream> GetAsync(int id)
        {
            PriceStream priceStream;
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
            {
                string query = @"
                    SELECT 
                        [Id],                         
                        [PriceStreamId],
                        [Exchange],
                        [Symbol],
                        [Price],
                        [OriginalPrice],
                        [CurrentDateTime],
                        [TimeZone]
                      FROM [dbo].[PriceStream]  WHERE Id = @Id";

                priceStream = await connection.QuerySingleAsync<PriceStream>(query, new {Id = id});
            }

            return priceStream;
        }
        
        public async Task<LocalDateTime?> GetLatestAsync(string symbol)
        {
            LocalDateTime? latest;
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
            {
                string query = @"
                    SELECT ISNULL(MAX(currentDateTime), '1900-01-01 00:00:00.0000000') as latest
                      FROM [dbo].[PriceStream]  WHERE symbol = @symbol";

                latest = await connection.QuerySingleAsync<LocalDateTime?>(query, new {symbol});
            }

            return latest;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await base.ExistsAsync(id, "PriceStream");
        }

        public async Task<int> CountAsync()
        {
            return await base.CountAsync("PriceStream");
        }

        public override string GetConnectionString() => _getDatabase.GetConnectionString();
    }
}