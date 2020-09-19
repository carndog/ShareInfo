using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage.Queries
{
    public class DuplicateHalifaxTransactionExistsQuery : Database, IDuplicateHalifaxTransactionExistsQuery
    {
        public async Task<bool> GetAsync(HalifaxTransaction transaction)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                      COUNT(*)
                      FROM [dbo].[HalifaxTransaction]  
                    WHERE Reference = @Reference
                ";

                count = await connection.QuerySingleAsync<int>(
                    query,
                    new
                    {
                        transaction.Reference
                    });
            }

            return count != 0;
        }
    }
}