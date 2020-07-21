using System.Configuration;

namespace Storage
{
    public abstract class Database
    {
        protected virtual string ConnectionString { get; } = ConfigurationManager.AppSettings["connectionString"];
    }
}