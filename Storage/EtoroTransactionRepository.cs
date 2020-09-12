using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace Storage
{
    public class EtoroTransactionRepository : Database, IEtoroTransactionRepository
    {
        public async Task<int> AddAsync(EtoroTransaction transaction)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string insertQuery = @"
                    DECLARE @InsertedRows AS TABLE (Id int);

                    INSERT INTO [dbo].[EtoroTransaction] 
                    (
                        [Date], [AccountBalance], [Type], [Details], [PositionId], [Amount], [RealizedEquityChange], [RealizedEquity]
                    ) 
                    OUTPUT INSERTED.Id INTO @InsertedRows
                    VALUES 
                    (
                        @Date, @AccountBalance, @Type, @Details, @PositionId, @Amount, @RealizedEquityChange, @RealizedEquity
                    );

                    SELECT Id FROM @InsertedRows
                ";

                IEnumerable<int> results = await db.QueryAsync<int>(insertQuery,
                    new
                    {
                        transaction.Date,
                        transaction.AccountBalance,
                        transaction.Type,
                        transaction.Details,
                        transaction.PositionId,
                        transaction.Amount,
                        transaction.RealizedEquityChange,
                        transaction.RealizedEquity
                    });
                int id = results.Single();

                return id;
            }
        }

        public async Task<EtoroTransaction> GetAsync(int id)
        {
            EtoroTransaction transaction;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                        Date,
                        AccountBalance,
                        Type,
                        Details,
                        PositionId,
                        Amount,
                        RealizedEquityChange,
                        RealizedEquity
                      FROM [dbo].[EtoroTransaction]  WHERE Id = @Id";

                transaction = await connection.QuerySingleAsync<EtoroTransaction>(query, new {Id = id});
            }

            return transaction;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<object> records = await connection.QueryAsync<object>(
                    "SELECT 1 WHERE EXISTS (SELECT 1 FROM [dbo].[EtoroTransaction] WHERE ID = @id)", new {id});
                return records.Any();
            }
        }
    }
}