
namespace Fn.Users.Models
{
    public sealed class RemoteUsersDto
    {
        private string _users;
        private int _total;
        private int _skip;
        private int _limit;

        public string users
        {
            get { return _users;} 
            set { _users = value;}
        }

        public int total
        {
            get { return _total; }
            set { _total = value; }
        }
        public int skip
        {
            get { return _skip; }
            set { _skip = value; }
        }
        public int limit
        {
            get { return _limit; }
            set { _limit = value; }
        }
    }
}