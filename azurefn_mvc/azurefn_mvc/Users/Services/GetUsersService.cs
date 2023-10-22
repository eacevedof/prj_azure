using Fn.Users.Models;
using Fn.Users.Views;

namespace Fn.Users.Services
{
    public sealed class GetUsersService
    {
        private readonly UsersRepository _usersRepository;

        public GetUsersService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }


        public UsersIndexDto Invoke(GetUsersBySearchDto GetUsersBySearchDto)
        {
            return new UsersIndexDto();
        }
    }
}