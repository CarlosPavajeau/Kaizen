namespace Kaizen.Core.Exceptions.Client
{
    public class ClientNotRegister : HttpException
    {
        public ClientNotRegister() : base(500, "Error del servidor, cliente no registrado")
        {

        }
    }
}
