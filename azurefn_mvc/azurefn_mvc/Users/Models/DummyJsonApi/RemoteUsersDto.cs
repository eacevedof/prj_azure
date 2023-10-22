using System.Collections.Generic;

namespace Fn.Users.Models.DummyJsonApi
{
    public sealed class RemoteUsersDto
    {
        public object users { get; set; }
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }
}