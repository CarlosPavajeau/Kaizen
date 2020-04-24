using Kaizen.Domain.Entities;
using Kaizen.EditModels;

namespace Kaizen.ViewModels
{
    public class ClientViewModel : ClientEditModel
    {
        public string Id { get; set; }
        public ClientViewModel(Client client)
        {
            Id = client.Id;
            FirstName = client.FirstName;
            SecondName = client.SecondName;
            LastName = client.LastName;
            SecondLastName = client.SecondLastName;

            ClientType = client.ClientType;
            NIT = client.NIT;
            BusninessName = client.BusninessName;
            TradeName = client.TradeName;

            FirstPhoneNumber = client.FirstPhoneNumber;
            SecondPhoneNumber = client.SecondPhoneNumber;
            FirstLandLine = client.FirstLandLine;
            SecondLandLine = client.SecondLandLine;
        }
    }
}
