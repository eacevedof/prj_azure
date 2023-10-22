using System;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;

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
            //List<RemoteUserDto> remoteUsers = JsonSerializer.Deserialize<List<RemoteUserDto>>(remoteUsersDto.users.ToString());

            var usersFound = new List<Dictionary<string, string>>();
            List<RemoteUserDto> remoteUsers = JsonSerializer.Deserialize<List<RemoteUserDto>>(remoteUsersDto.users.ToString());
            /*
            foreach (var o in remoteUsersDto.users)
            {
                var x = o.ToString();
                List<RemoteUserDto> remoteUsers = JsonSerializer.Deserialize<List<RemoteUserDto>>(o.ToString());
                var dic = new Dictionary<string, string>();
                dic["id"] = _GetAttributeValue(o, "id");
                dic["name"] = _GetAttributeValue(o, "firstName") + " " + _GetAttributeValue(o, "lastName");
                dic["email"] = _GetAttributeValue(o, "email");
                usersFound.Add(dic);
            }
            */
            return usersFound;
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

        private string _GetAttributeValue(Object obj, string attribute)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                if (propertyName != attribute)
                    continue;
                object propertyValue = property.GetValue(obj);
                return propertyValue.ToString();
            }
            return "";
        }
    }
}