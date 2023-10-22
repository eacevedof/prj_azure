
namespace Fn.Users.Models
{
    public sealed class RemoteUserDto
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;

        public int id
        {
            get { return _id;} 
            set { _id = value;}
        }

        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}