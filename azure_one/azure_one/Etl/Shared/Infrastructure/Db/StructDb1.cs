using azure_one.Etl.Shared.Infrastructure.Log;
using System.Data.SqlClient;


namespace azure_one.Etl.Shared.Infrastructure.Db
{
    public struct StructDb1
    {
        private const string Server = "localhost";
        private const string Port = "1433";
        private const string Database = "local_laciahub";
        private const string Username = "sa";
        private const string Password = "EafEaf1234";
        public string GetConnectionString()
        {
            //string: verbindingstekenreeks
            // "Data Source=myServerAddress;Initial Catalog=myDatabase;User Id=myUsername;Password=myPassword;";
            string[] connectionParts = new string[]
            {
                $"Server:tcp:{StructDb1.Server},{StructDb1.Port}",
                $"Initial Catalog={StructDb1.Database}",
                $"Persist Security Info=False;User ID={StructDb1.Username}",
                $"Password={StructDb1.Password}",
                "MultipleActiveResultSets=False",
                "Encrypt=True",
                "TrustServerCertificate=False",
                "Connection Timeout=30"
            };

            //log.LogInformation("C# HTTP trigger function processed a request.");
            string connectionString = string.Join(";", connectionParts);
            Lg.Pr(connectionString, "connectionString");
            connectionString = this.GetConnectionStringBuilder().ConnectionString;
            Lg.Pr(connectionString, "connectionString 2");
            return connectionString;
        }

        private SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = StructDb1.Server;
            builder.InitialCatalog = StructDb1.Database;
            builder.UserID = StructDb1.Username;
            builder.Password = StructDb1.Password;
            return builder;
        }
    }
}

