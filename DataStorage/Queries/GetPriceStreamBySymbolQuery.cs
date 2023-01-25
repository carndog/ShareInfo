using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage.Queries
{
    public class GetPriceStreamBySymbolQuery : IGetPriceStreamBySymbolQuery
    {
        private readonly IGetDatabase _getDatabase;

        public GetPriceStreamBySymbolQuery(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }

        public async Task<PriceStreamCollection> GetAsync(string symbol)
        {
            IEnumerable<PriceStream> priceStreams;
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
            {
                string query = @"
                    SELECT Id, 
                           PriceStreamId,
                           Symbol,
                           Exchange,
                           Price, 
                           OriginalPrice, 
                           CurrentDateTime,
                           TimeZone
                      FROM [dbo].[PriceStream]  
                    WHERE symbol = @Symbol
                    ORDER BY CurrentDateTime
                ";

                priceStreams = await connection.QueryAsync<PriceStream>(
                    query,
                    new
                    {
                        symbol
                    });
            }

            return new PriceStreamCollection
            {
                PriceStreams = priceStreams ?? Enumerable.Empty<PriceStream>()
            };
        }
    }
}