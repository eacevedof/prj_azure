
namespace Fn.Users.Views
{
    public sealed class UsersListDto
    {
        private int _id;
        private string _name;
        private string _email;

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Email
        {
            get { return _email; }
        }
    }
}