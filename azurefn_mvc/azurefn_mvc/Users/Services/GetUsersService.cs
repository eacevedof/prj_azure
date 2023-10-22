using System.Collections.Generic;

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

        public UsersListDto Invoke(GetUsersBySearchDto getUsersBySearchDto)
        {
            List<UsersEntity> usersList = _usersRepository.GetUsersBySearchText(
                getUsersBySearchDto.Search
            );
            return UsersListDto.FromPrimitives(usersList);
        }
    }
}