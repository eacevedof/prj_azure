
using Fn.Users.Models;

namespace Fn.Users.Services
{
    public sealed class UserCreateDto
    {
        private int _id;
        private string _name;
        private string _email;

        public UserCreateDto(int id, string name, string email)
        {
            _id = id;
            _name = name;
            _email = email;
        }

        public static UserCreateDto FromPrimitives(string name, string email)
        {
            return new UserCreateDto(
                -1,
                name,
                email
            );
        }

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