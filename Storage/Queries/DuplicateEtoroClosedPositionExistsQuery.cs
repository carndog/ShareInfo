using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;
using DTO.Exceptions;

namespace Storage.Queries
{
    public class DuplicateEtoroClosedPositionExistsQuery : Database, IDuplicateEtoroClosedPositionExistsQuery
    {
        public async Task<bool> GetAsync(EtoroClosedPosition position)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                      COUNT(*)
                      FROM [dbo].[EtoroClosedPosition]  
                    WHERE positionId = @PositionId
                ";

                count = await connection.QuerySingleAsync<int>(
                    query, 
                    new
                    {
                        position.PositionId
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