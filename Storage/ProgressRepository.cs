using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace Storage
{
    public class ProgressRepository : RepositoryBase, IProgressRepository
    {
        public async Task<Progress> Get()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {   
                string sql = @"SELECT TOP 1 processedCount FROM [dbo].[Progress] order by [date] desc";
                
                Progress progress = await connection.QuerySingleAsync<Progress>(sql);

                return progress;
            }
        }
    }
}
