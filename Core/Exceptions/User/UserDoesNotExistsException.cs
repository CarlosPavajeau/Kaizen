using System;

namespace Kaizen.Core.Exceptions.User
{
    [Serializable]
    public class UserDoesNotExistsException : HttpException
    {
        public UserDoesNotExistsException() : base(400, "Usuario no registrado")
        {
        }
    }
}
