using Microsoft.Extensions.Configuration;

namespace DataStorage
{
    public class GetDatabase : IGetDatabase
    {
        private readonly IConfiguration _configuration;

        public GetDatabase(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string GetConnectionString() => _configuration.GetConnectionString("connectionString");
    }
}