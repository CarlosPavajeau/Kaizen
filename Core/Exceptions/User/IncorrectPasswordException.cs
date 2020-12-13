using System;

namespace Kaizen.Core.Exceptions.User
{
    [Serializable]
    public class IncorrectPasswordException : HttpException
    {
        public IncorrectPasswordException() : base(400, "Contraseña incorrecta, intentelo de nuevo")
        {
        }
    }
}
