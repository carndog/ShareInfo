using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DTO;

namespace Storage
{
    public class ShareExtractRepository : IShareExtractRepository
    {
        public void Add(ShareExtract extract)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string insertQuery = @"
                    INSERT INTO [dbo].[Customer]([Id], [LastName], [State], [City], [IsActive], [CreatedOn]) 
                    VALUES (@FirstName, @LastName, @State, @City, @IsActive, @CreatedOn)";

                var result = db.Execute(insertQuery, extract);
            }
        }
    }
}
