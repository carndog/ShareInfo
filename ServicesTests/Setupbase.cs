using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ServicesTests.NodatimeHandlers;

namespace ServicesTests
{
    public abstract class Setupbase
    {
        public abstract string TableName { get; protected set; }

        public void Initialise()
        {
            Dapper.SqlMapper.AddTypeHandler(new LocalDateTimeTypeHandler());
            Dapper.SqlMapper.AddTypeHandler(new LocalDateTypeHandler());
            Dapper.SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);

            string commandText = $@"
                DELETE FROM [{TableName}]
                DBCC CHECKIDENT ('{TableName}', RESEED, 0)
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