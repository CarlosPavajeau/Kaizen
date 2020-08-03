namespace Kaizen.Core.Exceptions.Client
{
    public class ClientAlreadyRegistered : HttpException
    {
        public ClientAlreadyRegistered() : base(409, "El cliente ya esta registrado.")
        {

        }

        public ClientAlreadyRegistered(string clientCode) : base(409, $"La indentidicación {clientCode} ya se encuentra registrada.")
        {

        }
    }
}
