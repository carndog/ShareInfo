using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage
{
    public class HalifaxDividendRepository : Repository, IHalifaxDividendRepository
    {
        private readonly IGetDatabase _getDatabase;

        public HalifaxDividendRepository(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }

        public async Task<int> AddAsync(HalifaxDividend transaction)
        {
            using (IDbConnection db = new SqlConnection(_getDatabase.GetConnectionString()))
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
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
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
            return await base.ExistsAsync(id, "HalifaxDividend");
        }
        
        public async Task<int> CountAsync()
        {
            return await base.CountAsync("HalifaxDividend");
        }
        
        public override string GetConnectionString() => _getDatabase.GetConnectionString();
    }
}