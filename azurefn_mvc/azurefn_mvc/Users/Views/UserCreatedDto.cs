using System;
using System.Collections.Generic;

using Fn.Users.Models;

namespace Fn.Users.Views
{
    public sealed class UserCreatedDto
    {
        private List<object> _list = new();

        public UserCreatedDto(List<UsersEntity> usersEntities)
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

        public static UserCreatedDto FromPrimitives(List<UsersEntity> userEntities)
        {
            return new UserCreatedDto(userEntities);
        }

        public List<object> List
        {
            get { return _list; }
        }

    }
}