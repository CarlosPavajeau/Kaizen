using System;
using System.Runtime.Serialization;

namespace Kaizen.Core.Exceptions.User
{
    [Serializable]
    public class UserDoesNotExistsException : HttpException
    {
        public UserDoesNotExistsException() : base(400, "Usuario no registrado")
        {
        }

        protected UserDoesNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
