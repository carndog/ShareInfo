using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DTO;

namespace Storage
{
    public class ProgressRepository : IProgressRepository
    {
        private static readonly string ConnectionString = ConfigurationManager.AppSettings["connectionString"];

        public Progress Get()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {   
                string sql = @"SELECT TOP 1 processedCount FROM [dbo].[Progress] order by [date] desc";
                
                Progress progress = connection.QuerySingle<Progress>(sql);

                return progress;
            }
        }
    }
}
