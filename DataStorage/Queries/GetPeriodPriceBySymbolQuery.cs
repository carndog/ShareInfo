using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage.Queries
{
    public class GetPeriodPriceBySymbolQuery : IGetPeriodPriceBySymbolQuery
    {
        private readonly IGetDatabase _getDatabase;

        public GetPeriodPriceBySymbolQuery(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }
        
        public async Task<PeriodPriceCollection> GetAsync(string symbol)
        {
            IEnumerable<PeriodPrice> periodPrices;
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
            {
                string query = @"
                    SELECT Id, 
                           PeriodPriceId,
                           Symbol,
                           PeriodType, 
                           [Open],
                           High, 
                           Low, 
                           [Close], 
                           Volume, 
                           Date
                      FROM [dbo].[PeriodPrice]  
                    WHERE symbol = @Symbol
                    ORDER BY date
                ";

                periodPrices = await connection.QueryAsync<PeriodPrice>(
                    query,
                    new
                    {
                        symbol
                    });
            }

            return new PeriodPriceCollection
            {
                PeriodPrices = periodPrices ?? Enumerable.Empty<PeriodPrice>()
            };
        }
    }
}