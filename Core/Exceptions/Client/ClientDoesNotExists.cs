namespace Kaizen.Core.Exceptions.Client
{
    public class ClientDoesNotExists : HttpException
    {
        public ClientDoesNotExists() : base(404, "El cliente no esta registrado")
        {

        }
    }
}
