using System;
namespace Fn.Shared.Infrastructure.Db.Contexts
{
    public sealed class ContextDto
    {
        public string Id { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}