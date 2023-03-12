using System;
using System.Collections.Generic;
using System.Linq;
using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Db.Contexts;

/*
 *
 *  private const string _server = "localhost";
        private const string _port = "1433";
        private const string _database = "local_staging";
        private const string _username = "sa";
        private const string _password = "EafEaf1234";
 */

public sealed class Context
{
    public string Server { get; set; }
    public string Port { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
}