using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;
using ServicesTests.NodatimeHandlers;

namespace ServicesTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Dapper.SqlMapper.AddTypeHandler(new LocalDateTimeTypeHandler());
            Dapper.SqlMapper.AddTypeHandler(new LocalDateTypeHandler());

            string commandText = @"
                DELETE FROM [EtoroClosedPosition]
                DELETE FROM [EtoroTransaction]
                DELETE FROM [HalifaxDividend]
                DELETE FROM [HalifaxTransaction]
                DELETE FROM [PeriodPrice]

                DBCC CHECKIDENT ('EtoroClosedPosition', RESEED, 0)
                DBCC CHECKIDENT ('EtoroTransaction', RESEED, 0)
                DBCC CHECKIDENT ('HalifaxDividend', RESEED, 0)
                DBCC CHECKIDENT ('HalifaxTransaction', RESEED, 0)
                DBCC CHECKIDENT ('PeriodPrice', RESEED, 0)
            ";
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}