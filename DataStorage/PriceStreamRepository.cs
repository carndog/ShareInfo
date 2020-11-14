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
        public async Task<int> AddAsync(PriceStream priceStream)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
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
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
        
        public async Task<LocalDate?> GetLatestAsync(string symbol)
        {
            LocalDate? latest;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT ISNULL(MAX(date), '1900-01-01') as latest
                      FROM [dbo].[PriceStream]  WHERE symbol = @symbol";

                latest = await connection.QuerySingleAsync<LocalDate?>(query, new {symbol});
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
    }
}