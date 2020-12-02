namespace Kaizen.Core.Exceptions.User
{
    public class IncorrectPassword : HttpException
    {
        public IncorrectPassword() : base(400, "Contraseña incorrecta, intentelo de nuevo")
        {
        }
    }
}
