using Microsoft.Extensions.Configuration;

namespace IntegrationTests.ConnectionHelpers
{
    internal class ConnectionHelper
    {
        public static string GetConnnectionString()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = config["ConnectionStrings:Task13Db"];

            return connectionString;
        }
    }
}
