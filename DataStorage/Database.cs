using System.Configuration;

namespace DataStorage
{
    public abstract class Database
    {
        protected virtual string ConnectionString { get; } = ConfigurationManager.AppSettings["connectionString"];
    }
}