using System;
using System.Runtime.Serialization;

namespace Kaizen.Core.Exceptions.User
{
    [Serializable]
    public class IncorrectPasswordException : HttpException
    {
        public IncorrectPasswordException() : base(400, "Contraseña incorrecta, intentelo de nuevo")
        {
        }

        protected IncorrectPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
