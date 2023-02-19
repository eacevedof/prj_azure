using System;
namespace azure_one.Etl.Infrastructure.Db
{
    public struct StructDb1
    {
        // string connectionString = "Server=tcp:yourserver.database.windows.net,1433;
        // Initial Catalog=yourdatabase;Persist Security Info=False;User ID=yourusername;
        // Password=yourpassword;MultipleActiveResultSets=False;Encrypt=True;
        // TrustServerCertificate=False;Connection Timeout=30;";

        private const string Server = "localhost";
        private const string Port = "1433";
        private const string Database = "local_laciahub";
        private const string Username = "sa";
        private const string Password = "EafEaf1234";

        public string GetConnectionString()
        {
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

            return string.Join(";", connectionParts);
        }
    }
}

