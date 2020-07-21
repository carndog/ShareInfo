using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;
using DTO.Exceptions;

namespace Storage.Queries
{
    public class DuplicatePriceExistsQuery : Database, IDuplicatePriceExistsQuery
    {
        public async Task<bool> GetAsync(AssetPrice price)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                      COUNT(*)
                      FROM [dbo].[Prices]  
                    WHERE AssetId = @AssetId OR 
                      (Symbol = @Symbol AND Exchange = @Exchange AND 
                       ABS(DATEDIFF(minute, CurrentDateTime, @CurrentDateTime)) < 5)
                ";

                count = await connection.QuerySingleAsync<int>(
                    query, 
                    new
                    {
                        AssetId = price.AssetId,
                        Symbol = price.Symbol,
                        Exchange = price.Exchange,
                        Price = price.Price,
                        CurrentDateTime = price.CurrentDateTime
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