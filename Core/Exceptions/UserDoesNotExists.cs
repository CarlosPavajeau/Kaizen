namespace Kaizen.Core.Exceptions
{
    public class UserDoesNotExists : HttpException
    {
        public UserDoesNotExists() : base(400, "Usuario no registrado")
        {

        }
    }
}
