using System;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;

using Fn.Users.Models.DummyJsonApi;

namespace Fn.Users.Models
{
    public sealed class UsersRepository
    {
        private string USERS_ENDPOINT = "https://dummyjson.com/users?limit=5";

        public List<UsersEntity> GetUsersBySearchText(string searchText)
        {
            var json = _GetUsersJsonFromEndpoint();
            RemoteUsersDto remoteUsersDto = JsonSerializer.Deserialize<RemoteUsersDto>(json);
            List<RemoteUserDto> remoteUsers = JsonSerializer.Deserialize<List<RemoteUserDto>>(remoteUsersDto.users.ToString());

            var usersFound = new List<UsersEntity>();            
            foreach (var remoteUserDto in remoteUsers)
            {
                if (
                    remoteUserDto.id.ToString().Contains(searchText) ||
                    remoteUserDto.firstName.Contains(searchText) ||
                    remoteUserDto.lastName.Contains(searchText) ||
                    remoteUserDto.email.Contains(searchText) 
                )
                    usersFound.Add(UsersEntity.FromPrimitives(
                        remoteUserDto.id,
                        $"{remoteUserDto.firstName} {remoteUserDto.lastName}",
                        remoteUserDto.email
                    ));
            }
            return usersFound;
        }

        private string _GetUsersJsonFromEndpoint()
        {
            string json = "";
            using (WebClient client = new())
            {
                json = client.DownloadString(USERS_ENDPOINT);
            }
            Console.WriteLine(json);
            return json;
        }

        public UsersEntity CreateUser(UsersEntity userEntity)
        {
            userEntity.Id = _GetNewId();
            return userEntity;
        }

        private int _GetNewId()
        {
            Random random = new Random();
            return random.Next(1, 100000);
        }
    }
}