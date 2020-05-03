namespace Kaizen.Core.Exceptions
{
    public class IncorrectPassword : HttpException
    {
        public IncorrectPassword() : base(400, "Contraseña incorrecta, intentelo de nuevo")
        {

        }
    }
}
