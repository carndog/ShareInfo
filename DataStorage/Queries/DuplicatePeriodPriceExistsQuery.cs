using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DTO;
using DTO.Exceptions;

namespace DataStorage.Queries
{
    public class DuplicatePeriodPriceExistsQuery : IDuplicatePeriodPriceExistsQuery
    {
        private readonly IGetDatabase _getDatabase;

        public DuplicatePeriodPriceExistsQuery(IGetDatabase getDatabase)
        {
            _getDatabase = getDatabase;
        }

        public async Task<bool> GetAsync(PeriodPrice periodPrice)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_getDatabase.GetConnectionString()))
            {
                string query = @"
                    SELECT 
                      COUNT(*)
                      FROM [dbo].[PeriodPrice]  
                    WHERE PeriodPriceId = @PeriodPriceId OR 
                      (Symbol = @Symbol AND Date = @Date)
                ";

                count = await connection.QuerySingleAsync<int>(
                    query, 
                    new
                    {
                        periodPrice.PeriodPriceId,
                        periodPrice.Symbol,
                        periodPrice.Date
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