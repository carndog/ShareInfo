using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;
using DTO.Exceptions;

namespace DataStorage.Queries
{
    public class DuplicatePriceExistsQuery : IDuplicatePriceExistsQuery
    {
        private readonly IGetDatabase _getDatabase;

        public DuplicatePriceExistsQuery(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }

        public async Task<bool> GetAsync(AssetPrice price)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
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
                        price.AssetId,
                        price.Symbol,
                        price.Exchange,
                        price.Price,
                        price.CurrentDateTime
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