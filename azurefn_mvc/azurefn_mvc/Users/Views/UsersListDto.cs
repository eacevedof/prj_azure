using System;
using System.Collections.Generic;

using Fn.Users.Models;

namespace Fn.Users.Views
{
    public sealed class UsersListDto
    {
        private List<object> _list = new();

        public UsersListDto(List<UsersEntity> usersEntities)
        {
            foreach(UsersEntity userEntity in usersEntities)
            {
                Object obj = new
                {
                    identifier = userEntity.Id,
                    full_name = userEntity.Name,
                    personal_email = userEntity.Email
                };
                _list.Add(obj);
            }
        }

        public static UsersListDto FromPrimitives(List<UsersEntity> userEntities)
        {
            return new UsersListDto(userEntities);
        }

        public List<object> Users
        {
            get { return _list; }
        }

    }
}