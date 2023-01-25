using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage.Queries
{
    public class DuplicateHalifaxDividendExistsQuery : IDuplicateHalifaxDividendExistsQuery
    {
        private readonly IGetDatabase _getDatabase;

        public DuplicateHalifaxDividendExistsQuery(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }

        public async Task<bool> GetAsync(HalifaxDividend dividend)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
            {
                string query = @"
                    SELECT 
                      COUNT(*)
                      FROM [dbo].[HalifaxDividend]  
                    WHERE ExDividendDate = @ExDividendDate AND Stock = @Stock
                ";

                count = await connection.QuerySingleAsync<int>(
                    query,
                    new
                    {
                        dividend.ExDividendDate,
                        dividend.Stock
                    });
            }

            return count != 0;
        }
    }
}