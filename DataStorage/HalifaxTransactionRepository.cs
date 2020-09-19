using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;

namespace DataStorage
{
    public class HalifaxTransactionRepository : Database, IHalifaxTransactionRepository
    {
        public async Task<int> AddAsync(HalifaxTransaction transaction)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string insertQuery = @"
                    DECLARE @InsertedRows AS TABLE (Id int);

                    INSERT INTO [dbo].[HalifaxTransaction] 
                    (
                        [Date], [Type], [CompanyCode], [Exchange], [Quantity], [ExecutedPrice], [NetConsideration], [Reference]
                    ) 
                    OUTPUT INSERTED.Id INTO @InsertedRows
                    VALUES 
                    (
                        @Date, @Type, @CompanyCode, @Exchange, @Quantity, @ExecutedPrice, @NetConsideration, @Reference
                    );

                    SELECT Id FROM @InsertedRows
                ";

                IEnumerable<int> results = await db.QueryAsync<int>(insertQuery,
                    new
                    {
                        transaction.Date,
                        transaction.Type,
                        transaction.CompanyCode,
                        transaction.Exchange,
                        transaction.Quantity,
                        transaction.ExecutedPrice,
                        transaction.NetConsideration,
                        transaction.Reference
                    });
                int id = results.Single();

                return id;
            }
        }

        public async Task<HalifaxTransaction> GetAsync(int id)
        {
            HalifaxTransaction transaction;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                        Date,
                        Type,
                        CompanyCode,
                        Exchange,
                        Quantity,
                        ExecutedPrice,
                        NetConsideration,
                        Reference
                      FROM [dbo].[HalifaxTransaction]  WHERE Id = @Id";

                transaction = await connection.QuerySingleAsync<HalifaxTransaction>(query, new {Id = id});
            }

            return transaction;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                IEnumerable<object> records = await connection.QueryAsync<object>(
                    "SELECT 1 WHERE EXISTS (SELECT 1 FROM [dbo].[HalifaxTransaction] WHERE ID = @id)", new {id});
                return records.Any();
            }
        }
    }
}