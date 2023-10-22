using System.Collections.Generic;

namespace Fn.Users.Models
{
    public sealed class RemoteUsersDto
    {
        public List<object> users { get; set; }
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }
}