using System.Configuration;

namespace Storage
{
    public abstract class RepositoryBase
    {
        protected virtual string ConnectionString { get; } = ConfigurationManager.AppSettings["connectionString"];
    }
}