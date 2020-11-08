using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DTO;
using NodaTime;

namespace DataStorage
{
    public class PeriodPriceRepository : Repository, IPeriodPriceRepository
    {
        public async Task<int> AddAsync(PeriodPrice periodPrice)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                string insertQuery = @"
                    DECLARE @InsertedRows AS TABLE (Id int);

                    INSERT INTO [dbo].[PeriodPrice] 
                    (
                        [PeriodPriceId], [Symbol], [PeriodType], [Open], [High], [Low], [Close], [Volume], [Date]
                    ) 
                    OUTPUT INSERTED.Id INTO @InsertedRows
                    VALUES 
                    (
                        @PeriodPriceId, @Symbol, @PeriodType, @Open, @High, @Low, @Close, @Volume, @Date
                    );

                    SELECT Id FROM @InsertedRows
                ";

                IEnumerable<int> results = await db.QueryAsync<int>(insertQuery,
                    new
                    {
                        periodPrice.PeriodPriceId,
                        periodPrice.Symbol,
                        periodPrice.PeriodType,
                        periodPrice.Open,
                        periodPrice.High,
                        periodPrice.Low,
                        periodPrice.Close,
                        periodPrice.Volume,
                        periodPrice.Date
                    });
                int id = results.Single();

                return id;
            }
        }

        public async Task<PeriodPrice> GetAsync(int id)
        {
            PeriodPrice periodPrice;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT 
                        [Id],                         
                        [PeriodPriceId],
                        [Symbol],
                        [PeriodType],
                        [Open],
                        [High],
                        [Low],
                        [Close],
                        [Volume],
                        [Date]
                      FROM [dbo].[PeriodPrice]  WHERE Id = @Id";

                periodPrice = await connection.QuerySingleAsync<PeriodPrice>(query, new {Id = id});
            }

            return periodPrice;
        }
        
        public async Task<LocalDate?> GetLatestAsync(string symbol)
        {
            LocalDate? latest;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT ISNULL(MAX(date), '1900-01-01') as latest
                      FROM [dbo].[PeriodPrice]  WHERE symbol = @symbol";

                latest = await connection.QuerySingleAsync<LocalDate?>(query, new {symbol});
            }

            return latest;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await base.ExistsAsync(id, "PeriodPrice");
        }

        public async Task<int> CountAsync()
        {
            return await base.CountAsync("PeriodPrice");
        }
    }
}