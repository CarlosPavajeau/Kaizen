using Kaizen.Domain.Entities;
using Kaizen.InputModels;
using System.Collections.Generic;
using System.Linq;

namespace Kaizen.ViewModels
{
    public class ClientViewModel : ClientInputModel
    {
        public ClientViewModel(Client client)
        {
            Id = client.Id;
            FirstName = client.FirstName;
            SecondName = client.SecondName;
            LastName = client.LastName;
            SeconLastname = client.SeconLastname;

            ClientType = client.ClientType;
            BusninessName = client.BusninessName;
            TradeName = client.TradeName;

            FirstPhoneNumber = client.FirstPhoneNumber;
            SecondPhoneNumber = client.SecondPhoneNumber;
            FirstLandLine = client.FirstLandLine;
            SecondLandLine = client.SecondLandLine;

            ClientAddress = new ClientAddressViewModel(client.ClientAddress);
            ContactPeople = (from c in client.ContactPeople
                             select new ContactPersonViewModel(c)).ToList();
        }

        public new ClientAddressViewModel ClientAddress { get; set; }
        public new List<ContactPersonViewModel> ContactPeople { get; set; }
    }
}
