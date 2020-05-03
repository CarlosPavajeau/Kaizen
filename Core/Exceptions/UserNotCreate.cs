using System;

namespace Kaizen.Core.Exceptions
{
    public class UserNotCreate : HttpException
    {
        public UserNotCreate() : base(400, "Usuario no creado")
        {

        }
    }
}
