namespace Kaizen.Core.Exceptions.User
{
    public class UserDoesNotExists : HttpException
    {
        public UserDoesNotExists() : base(400, "Usuario no registrado")
        {

        }
    }
}
