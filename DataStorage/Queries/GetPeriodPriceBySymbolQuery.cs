using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage.Queries
{
    public class GetPeriodPriceBySymbolQuery : Database, IGetPeriodPriceBySymbolQuery
    {
        public async Task<IEnumerable<PeriodPrice>> GetAsync(string symbol)
        {
            IEnumerable<PeriodPrice> periodPrices;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                    ORDER BY date desc
                ";

                periodPrices = await connection.QueryAsync<PeriodPrice>(
                    query,
                    new
                    {
                        symbol
                    });
            }

            return periodPrices ?? Enumerable.Empty<PeriodPrice>();
        }
    }
}