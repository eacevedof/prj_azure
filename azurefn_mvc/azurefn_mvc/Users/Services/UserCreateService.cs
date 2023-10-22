using Fn.Users.Models;

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
            var userToCreateEntity = UsersEntity.FromUserCreate(userCreateDto);
            UsersEntity userCreatedEntity = _usersRepository.CreateUser(
                userToCreateEntity
            );
            return userCreatedEntity;
        }
    }
}