namespace Kaizen.Core.Exceptions.Client
{
    public class ClientDoesNotExists : HttpException
    {
        public ClientDoesNotExists() : base(404, "El cliente no esta registrado.")
        {

        }

        public ClientDoesNotExists(string id) : base(404, $"El cliente con identificación {id} no se encuentra registrado.")
        {

        }
    }
}
