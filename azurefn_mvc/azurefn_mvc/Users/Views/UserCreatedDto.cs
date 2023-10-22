using System;
using System.Collections.Generic;

using Fn.Users.Models;

namespace Fn.Users.Views
{
    public sealed class UserCreatedDto
    {
        private Object _user;

        public UserCreatedDto(UsersEntity userEntity)
        {
            Object obj = new
            {
                identifier = userEntity.Id,
                full_name = userEntity.Name,
                personal_email = userEntity.Email
            };
            
        }

        public static UserCreatedDto FromPrimitives(UsersEntity userEntity)
        {
            return new UserCreatedDto(userEntity);
        }

        public Object User
        {
            get { return _user; }
        }

    }
}