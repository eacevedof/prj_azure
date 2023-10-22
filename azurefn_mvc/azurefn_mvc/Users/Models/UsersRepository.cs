using System.Collections.Generic;

namespace Fn.Users.Models
{

    public sealed class UsersRepository
    {
        public List<Dictionary<string,string>> GetUsersBySearchText(string searchText)
        {
            List<Dictionary<string, string>> list = new();
            return list;
        }
    }
}