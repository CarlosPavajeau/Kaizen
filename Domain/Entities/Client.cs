using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class Client : Person
    {
        [MaxLength(30)]
        public string NIT { get; set; }
        [MaxLength(20)]
        public string ClientType { get; set; }
        [MaxLength(50)]
        public string BusninessName { get; set; }
        [MaxLength(50)]
        public string TradeName { get; set; }
        [Required, MaxLength(10)]
        public string FirstPhoneNumber { get; set; }
        [MaxLength(10)]
        public string SecondPhoneNumber { get; set; }
        [MaxLength(15)]
        public string FirstLandLine { get; set; }
        [MaxLength(15)]
        public string SecondLandLine { get; set; }

        public ClientAddress ClientAddress { get; set; }
        public List<ContactPerson> ContactPeople { get; set; }
    }
}
