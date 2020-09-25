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

            string commandText = @"
                DELETE FROM [EtoroClosedPosition]
                DELETE FROM [EtoroTransaction]
                DELETE FROM [HalifaxDividend]
                DELETE FROM [HalifaxTransaction]

                DBCC CHECKIDENT ('EtoroClosedPosition', RESEED, 0)
                DBCC CHECKIDENT ('EtoroTransaction', RESEED, 0)
                DBCC CHECKIDENT ('HalifaxDividend', RESEED, 0)
                DBCC CHECKIDENT ('HalifaxTransaction', RESEED, 0)
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