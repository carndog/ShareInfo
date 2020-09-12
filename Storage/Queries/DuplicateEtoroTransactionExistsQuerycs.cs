using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Storage.Queries
{
    public class EtoroTransactionExistsQuery : Database, IEtoroTransactionExistsQuery
    {
        public async Task<bool> GetAsync()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                      COUNT(*)
                      FROM [dbo].[EtoroTransaction]
                ";

                count = await connection.QuerySingleAsync<int>(query);
            }

            return count != 0;
        }
    }
}