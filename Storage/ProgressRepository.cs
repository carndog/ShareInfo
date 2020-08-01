using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace Storage
{
    public class ProgressRepository : Database, IProgressRepository
    {
        public async Task<Progress> Get()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {   
                string sql = @"select count(*) from [dbo].[Prices]";
                
                Progress progress = await connection.QuerySingleAsync<Progress>(sql);

                return progress;
            }
        }
    }
}
