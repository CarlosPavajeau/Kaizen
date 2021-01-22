using System;
using System.Runtime.Serialization;

namespace Kaizen.Core.Exceptions.User
{
    [Serializable]
    public class UserNotCreateException : HttpException
    {
        public UserNotCreateException() : base(400, "Usuario no creado")
        {
        }

        protected UserNotCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
