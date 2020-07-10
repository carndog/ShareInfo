using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DTO;

namespace Storage
{
    public class PriceRepository : IPriceRepository
    {
        public void Add(AssetPrice extract)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string insertQuery = @"
                    INSERT INTO [dbo].[Customer]([Id], [LastName], [State], [City], [IsActive], [CreatedOn]) 
                    VALUES (@FirstName, @LastName, @State, @City, @IsActive, @CreatedOn)";

                int result = db.Execute(insertQuery, extract);
            }
        }
    }
}
