using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage.Queries
{
    public class GetPriceStreamBySymbolQuery : Database, IGetPriceStreamBySymbolQuery
    {
        public async Task<PriceStreamCollection> GetAsync(string symbol)
        {
            IEnumerable<PriceStream> priceStreams;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT Id, 
                           PriceStreamId,
                           Symbol,
                           Exchange,
                           Price, 
                           OriginalPrice, 
                           Date
                      FROM [dbo].[PriceStream]  
                    WHERE symbol = @Symbol
                    ORDER BY date
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