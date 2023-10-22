using System;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;


namespace Fn.Users.Models
{
    public sealed class UsersRepository
    {
        private string USERS_ENDPOINT = "https://dummyjson.com/users?limit=15";

        public List<Dictionary<string,string>> GetUsersBySearchText(string searchText)
        {
            var json = _GetUsersJsonFromEndpoint();
            List<Dictionary<string, string>> list = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
            return list;
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