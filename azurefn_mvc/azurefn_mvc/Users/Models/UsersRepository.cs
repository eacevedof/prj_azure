using System;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;


namespace Fn.Users.Models
{
    public sealed class UsersRepository
    {
        private string USERS_ENDPOINT = "https://dummyjson.com/users";

        public List<Dictionary<string,string>> GetUsersBySearchText(string searchText)
        {
            var json = _GetUsersFromEndpoint();
            List<Dictionary<string, string>> list = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
            return list;
        }

        private string _GetUsersFromEndpoint()
        {
            string content = "";
            using (WebClient client = new WebClient())
            {
                content = client.DownloadString(USERS_ENDPOINT);
                Console.WriteLine(content);
            }
            return content;
        }

    }
}