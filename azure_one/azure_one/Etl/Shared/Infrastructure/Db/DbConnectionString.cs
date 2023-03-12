using System;
using System.Data.SqlClient;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;

namespace azure_one.Etl.Shared.Infrastructure.Db
{
    public readonly struct DbConnectionString
    {
        private readonly ContextDto _contextDto;

        public DbConnectionString(ContextDto contextDto)
        {
            _contextDto = contextDto;
        }        
        
        public string GetStringOrFail()
        {
            if (_contextDto is null)
                throw new Exception($"Empty context");
            
            string connectionString = GetConnectionStringBuilder(_contextDto).ConnectionString;
            Lg.pr(connectionString, "\nconnectionString by dto\n");
            return connectionString;
        }
        
        private SqlConnectionStringBuilder GetConnectionStringBuilder(ContextDto contextDto)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = contextDto.Server;
            builder.InitialCatalog = contextDto.Database;
            builder.UserID = contextDto.Username;
            builder.Password = contextDto.Password;
            builder.Pooling = false;
            builder.Encrypt = false;
            builder.IntegratedSecurity = false;
            builder.TrustServerCertificate = true;
            return builder;
        }
    }
}

