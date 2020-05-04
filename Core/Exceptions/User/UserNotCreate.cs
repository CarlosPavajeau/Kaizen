namespace Kaizen.Core.Exceptions.User
{
    public class UserNotCreate : HttpException
    {
        public UserNotCreate() : base(400, "Usuario no creado")
        {

        }
    }
}
