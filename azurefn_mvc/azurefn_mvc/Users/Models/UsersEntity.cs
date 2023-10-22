using Fn.Users.Services;

namespace Fn.Users.Models
{

    public sealed class UsersEntity
    {
        private int _id;
        private string _name;
        private string _email;

        public UsersEntity(int id, string name, string email)
        {
            _id = id;
            _name = name;
            _email = email;
        }

        public UsersEntity() {}

        public static UsersEntity FromPrimitives(int id, string name, string email)
        {
            return new UsersEntity(
                id,
                name,
                email
            );
        }

        public static UsersEntity FromUserCreate(UserCreateDto userCreateDto)
        {
            return new UsersEntity(
                -1,
                userCreateDto.Name,
                userCreateDto.Email
            );
        }

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