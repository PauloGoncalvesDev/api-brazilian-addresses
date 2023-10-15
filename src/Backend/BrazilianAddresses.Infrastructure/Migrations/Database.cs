using Dapper;
using System.Data.SqlClient;

namespace BrazilianAddresses.Infrastructure.Migrations
{
    public static class Database
    {
        public static void CreateDatabase(string connectionString, string databaseName)
        {
			try
			{
                using var myConnection = new SqlConnection(connectionString);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("name", databaseName);
                                
                var results = myConnection.Query("SELECT name FROM sys.databases WHERE name = @name", parameters);

                if (!results.Any())
                {
                    myConnection.Execute($"CREATE DATABASE {databaseName}");
                }
            }
			catch (Exception ex)
			{
                				
			}
        }
    }
}
