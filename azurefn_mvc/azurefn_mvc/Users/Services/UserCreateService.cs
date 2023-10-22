using System.Collections.Generic;

using Fn.Users.Models;
using Fn.Users.Views;

namespace Fn.Users.Services
{
    public sealed class UserCreateService
    {
        private readonly UsersRepository _usersRepository;

        public UserCreateService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public UsersEntity Invoke(UserCreateDto userCreateDto)
        {
            var userToCreate = UsersEntity.FromUserCreate(userCreateDto);
            UsersEntity userCreatedEntity = _usersRepository.CreateUser(
                userToCreate
            );
            return userCreatedEntity;
        }
    }
}