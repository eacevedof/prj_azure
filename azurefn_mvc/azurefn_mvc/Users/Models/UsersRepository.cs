using System;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;


namespace Fn.Users.Models
{
    public sealed class UsersRepository
    {
        private string USERS_ENDPOINT = "https://dummyjson.com/users?limit=3";

        public List<Dictionary<string,string>> GetUsersBySearchText(string searchText)
        {
            var json = _GetUsersJsonFromEndpoint();
            //List<Object> list = JsonSerializer.Deserialize<List<Object>>(json);

            RemoteUsersDto remoteUsersDto = JsonSerializer.Deserialize<RemoteUsersDto>(json);
            List<RemoteUserDto> remoteUsers = JsonSerializer.Deserialize<List<RemoteUserDto>>(remoteUsersDto.users);
            //var x = deserializedObject.users;
            // Accessing the list of users
            //List<User> users = deserializedObject.users;

            return new List<Dictionary<string, string>>();
        }

        private string _GetUsersJsonFromEndpoint()
        {
            string json = "";
            using (WebClient client = new())
            {
                json = client.DownloadString(USERS_ENDPOINT);

                Console.WriteLine(json);
            }
            return json;
        }

    }
}