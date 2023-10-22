using System;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;

using Fn.Users.Models.Dummyjson;

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
                var userEntity = new UsersEntity();
                userEntity.Id = remoteUserDto.id;
                userEntity.Name = $"{remoteUserDto.firstName} {remoteUserDto.lastName}";
                userEntity.Email = remoteUserDto.email;
                usersFound.Add(userEntity);
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
    }
}