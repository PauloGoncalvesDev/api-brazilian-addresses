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

                // TODO: Implementar alguma forma de validar se o DB existe, essa aqui não tá funcionando
                var results = myConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

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
