using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;
using DTO.Exceptions;

namespace DataStorage.Queries
{
    public class DuplicatePriceStreamExistsQuery : IDuplicatePriceStreamExistsQuery
    {
        private readonly IGetDatabase _getDatabase;

        public DuplicatePriceStreamExistsQuery(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }

        public async Task<bool> GetAsync(PriceStream priceStream)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
            {
                string query = @"
                    SELECT 
                      COUNT(*)
                      FROM [dbo].[PriceStream]  
                    WHERE PriceStreamId = @PriceStreamId OR 
                      (Symbol = @Symbol AND CurrentDateTime = @CurrentDateTime AND TimeZone = @TimeZone)
                ";

                count = await connection.QuerySingleAsync<int>(
                    query, 
                    new
                    {
                        priceStream.PriceStreamId,
                        priceStream.Symbol,
                        priceStream.CurrentDateTime,
                        priceStream.TimeZone
                    });
            }

            if (count > 1)
            {
                throw new DuplicateExistsException($"{count}");
            }
            
            return count == 1;
        }
    }
}