using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage
{
    public class HalifaxDividendRepository : Database, IHalifaxDividendRepository
    {
        public async Task<int> AddAsync(HalifaxDividend transaction)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string insertQuery = @"
                    DECLARE @InsertedRows AS TABLE (Id int);

                    INSERT INTO [dbo].[HalifaxDividend] 
                    (
                        [IssueDate], [ExDividendDate], [Stock], [SharesHeld], [Amount], [HandlingOption]
                    ) 
                    OUTPUT INSERTED.Id INTO @InsertedRows
                    VALUES 
                    (
                        @IssueDate, @ExDividendDate, @Stock, @SharesHeld, @Amount, @HandlingOption
                    );

                    SELECT Id FROM @InsertedRows
                ";

                IEnumerable<int> results = await db.QueryAsync<int>(insertQuery,
                    new
                    {
                        transaction.IssueDate,
                        transaction.ExDividendDate,
                        transaction.Stock,
                        transaction.SharesHeld,
                        transaction.Amount,
                        transaction.HandlingOption
                    });
                int id = results.Single();

                return id;
            }
        }

        public async Task<HalifaxDividend> GetAsync(int id)
        {
            HalifaxDividend transaction;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                        IssueDate,
                        ExDividendDate,
                        Stock,
                        SharesHeld,
                        Amount,
                        HandlingOption
                      FROM [dbo].[HalifaxDividend]  WHERE Id = @Id";

                transaction = await connection.QuerySingleAsync<HalifaxDividend>(query, new {Id = id});
            }

            return transaction;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<object> records = await connection.QueryAsync<object>(
                    "SELECT 1 WHERE EXISTS (SELECT 1 FROM [dbo].[HalifaxDividend] WHERE ID = @id)", new {id});
                return records.Any();
            }
        }
    }
}