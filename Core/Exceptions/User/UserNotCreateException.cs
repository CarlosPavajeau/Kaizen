using System;

namespace Kaizen.Core.Exceptions.User
{
    [Serializable]
    public class UserNotCreateException : HttpException
    {
        public UserNotCreateException() : base(400, "Usuario no creado")
        {
        }
    }
}
