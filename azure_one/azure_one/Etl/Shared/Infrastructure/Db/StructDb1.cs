using azure_one.Etl.Shared.Infrastructure.Log;
using System.Data.SqlClient;


namespace azure_one.Etl.Shared.Infrastructure.Db
{
    public struct StructDb1
    {
        private const string _server = "localhost";
        private const string _port = "1433";
        private const string _database = "local_staging";
        private const string _username = "sa";
        private const string _password = "EafEaf1234";
        
        public string GetConnectionString()
        {
            string[] connectionParts = new string[]
            {
                $"_server:tcp:{StructDb1._server},{StructDb1._port}",
                $"Initial Catalog={StructDb1._database}",
                $"Persist Security Info=False;User ID={StructDb1._username}",
                $"_password={StructDb1._password}",
                "MultipleActiveResultSets=False",
                "Encrypt=True",
                "TrustServerCertificate=False",
                "Connection Timeout=30"
            };

            //log.LogInformation("C# HTTP trigger function processed a request.");
            string connectionString = string.Join(";", connectionParts);
            //Lg.pr(connectionString, "connectionString");
            
            //Server=localhost; Initial Catalog={0}; Integrated Security=true; pooling=false;";
            connectionString = GetConnectionStringBuilder().ConnectionString;
            Lg.pr(connectionString, "connectionString 2");
            return connectionString;
        }

        private SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = _server;
            builder.InitialCatalog = _database;
            builder.UserID = _username;
            builder.Password = _password;
            builder.IntegratedSecurity = false;
            builder.Pooling = false;
            builder.Encrypt = false;
            builder.TrustServerCertificate = true;
            return builder;
        }
    }
}

