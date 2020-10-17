using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DataStorage
{
    public abstract class Repository : Database
    {
        protected virtual async Task<bool> ExistsAsync(int id, string table)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<object> records = await connection.QueryAsync<object>(
                    $"SELECT 1 WHERE EXISTS (SELECT 1 FROM [dbo].[{table}] WHERE ID = @id)", new {id});
                return records.Any();
            }
        }
        
        protected virtual async Task<int> CountAsync(string table)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return await connection.QuerySingleAsync<int>($"SELECT COUNT(*) FROM [dbo].[{table}]");
            }
        }
    }
}