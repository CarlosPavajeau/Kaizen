using System.Collections.Generic;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.Client
{
    public class ClientEditModel : PersonEditModel
    {
        public string ClientType { get; set; }
        public string NIT { get; set; }
        public string BusninessName { get; set; }
        public string TradeName { get; set; }
        public string FirstPhoneNumber { get; set; }
        public string SecondPhoneNumber { get; set; }
        public string FirstLandLine { get; set; }
        public string SecondLandLine { get; set; }
        public ClientAddressModel ClientAddress { get; set; }
        public List<ContactPersonModel> ContactPeople { get; set; }
        public ClientState State { get; set; }
    }
}
