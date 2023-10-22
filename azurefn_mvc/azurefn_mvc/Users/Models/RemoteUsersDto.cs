
namespace Fn.Users.Models
{
    public sealed class RemoteUsersDto
    {
        public string users { get; set; }
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }
}