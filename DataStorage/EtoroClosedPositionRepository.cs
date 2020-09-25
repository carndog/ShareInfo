using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage
{
    public class EtoroClosedPositionRepository : Database, IEtoroClosedPositionRepository
    {
        public async Task<int> AddAsync(EtoroClosedPosition position)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string insertQuery = @"
                    DECLARE @InsertedRows AS TABLE (Id int);

                    INSERT INTO [dbo].[EtoroClosedPosition] 
                    (
                        [PositionId], [Action], [Amount], [Units], [OpenRate], [CloseRate], [Spread], [Profit], 
                        [OpenDate], [ClosedDate], [TakeProfitRate], [StopLossRate], [RolloverFees]
                    ) 
                    OUTPUT INSERTED.Id INTO @InsertedRows
                    VALUES 
                    (
                        @PositionId, @Action, @Amount, @Units, @OpenRate, @CloseRate, @Spread, @Profit, 
                        @OpenDate, @ClosedDate, @TakeProfitRate, @StopLossRate, @RolloverFees
                    );

                    SELECT Id FROM @InsertedRows
                ";

                IEnumerable<int> results = await db.QueryAsync<int>(insertQuery,
                    new
                    {
                        position.PositionId,
                        position.Action,
                        position.Amount,
                        position.Units,
                        position.OpenRate,
                        position.CloseRate,
                        position.Spread,
                        position.Profit,
                        position.OpenDate,
                        position.ClosedDate,
                        position.TakeProfitRate,
                        position.StopLossRate,
                        position.RollOverFees
                    });
                int id = results.Single();

                return id;
            }
        }

        public async Task<EtoroClosedPosition> GetAsync(int id)
        {
            EtoroClosedPosition position;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                        [PositionId],
                        [Action],
                        [Amount],
                        [Units],
                        [OpenRate],
                        [CloseRate],
                        [Spread],
                        [Profit],
                        [OpenDate],
                        [ClosedDate],
                        [TakeProfitRate],
                        [StopLossRate],
                        [RollOverFees]
                      FROM [dbo].[EtoroClosedPosition]  WHERE Id = @Id";

                position = await connection.QuerySingleAsync<EtoroClosedPosition>(query, new {Id = id});
            }

            return position;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await base.ExistsAsync(id, "EtoroClosedPosition");
        }

        public async Task<int> CountAsync()
        {
            return await base.CountAsync("EtoroClosedPosition");
        }
    }
}