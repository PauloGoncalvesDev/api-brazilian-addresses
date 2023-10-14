using Microsoft.Extensions.Configuration;

namespace BrazilianAddresses.Domain.Extension
{
    public static class RepositoryExtension
    {
        public static string GetConnectionString(this IConfiguration configurationManager)
        {
            var connection = configurationManager.GetConnectionString("Connection");

            return connection;
        }

        public static string GetDatabaseName(this IConfiguration configurationManager)
        {
            var databaseName = configurationManager.GetConnectionString("DatabaseName");

            return databaseName;
        }
    }
}
