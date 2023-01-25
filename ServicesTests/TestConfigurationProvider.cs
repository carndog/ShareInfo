using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;

namespace ServicesTests;

public static class TestConfigurationProvider
{
    public static IConfigurationRoot GetConfigurationRoot()
    {
        var myConfiguration = new Dictionary<string, string>
        {
            { "connectionString", "Server=Carndog;Database=SharesTest;Trusted_Connection=True;" },
        };

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();
        return configuration;
    }
}