
namespace Fn.Users.Models
{
    public sealed class UsersEntity
    {
        private int _id;
        private string _name;
        private string _email;

        public int Id
        {
            get { return _id;} 
            set { _id = value;}
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}