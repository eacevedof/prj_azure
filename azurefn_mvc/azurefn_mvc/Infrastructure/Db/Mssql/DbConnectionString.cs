using System;
using System.Data.SqlClient;

using azurefn_mvc.Infrastructure.Db.Contexts;

namespace azurefn_mvc.Infrastructure.Db.Mssql
{
    public readonly struct DbConnectionString
    {
        private const int _CONNECTION_TIMEOUT_SECONDS = 600;
        private readonly ContextDto _contextDto;

        public DbConnectionString(ContextDto contextDto)
        {
            _contextDto = contextDto;
        }

        public static DbConnectionString GetInstanceByContextDto(ContextDto contextDto)
        {
            return new DbConnectionString(contextDto);
        }

        public string GetStringOrFail()
        {
            if (_contextDto is null)
                throw new Exception($"GetStringOrFail: Empty contextDto");

            string connectionString = GetConnectionStringBuilder().ConnectionString;
            return connectionString;
        }

        private SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            //Data Source=zzzz.database.windows.net;Initial Catalog=dev_aaa;User id=yyy;Password=xxxx; connection timeout=5000;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = _contextDto.Server;
            builder.InitialCatalog = _contextDto.Database;
            builder.UserID = _contextDto.Username;
            builder.Password = _contextDto.Password;
            builder.ConnectTimeout = _CONNECTION_TIMEOUT_SECONDS;
            return builder;
        }
    }
}
