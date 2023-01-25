using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage.Queries
{
    public class DuplicateDuplicateEtoroTransactionExistsQuery : IDuplicateEtoroTransactionExistsQuery
    {
        private readonly IGetDatabase _getDatabase;

        public DuplicateDuplicateEtoroTransactionExistsQuery(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }

        public async Task<bool> GetAsync(EtoroTransaction transaction)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
            {
                string query = @"
                    SELECT 
                      COUNT(*)
                      FROM [dbo].[EtoroTransaction]  
                    WHERE PositionId = @PositionId AND Amount = @Amount AND Type = @Type
                        AND RealizedEquity = @RealizedEquity
                ";

                count = await connection.QuerySingleAsync<int>(
                    query,
                    new
                    {
                        transaction.PositionId,
                        transaction.Amount,
                        transaction.Type,
                        transaction.RealizedEquity
                    });
            }

            return count != 0;
        }
    }
}