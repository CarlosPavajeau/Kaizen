using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kaizen.Domain.Entities;
using Kaizen.Models.Base;

namespace Kaizen.Models.Client
{
    public class ClientEditModel : PersonEditModel
    {
        [Required(ErrorMessage = "El tipo de usuario es requerido")]
        public string ClientType { get; set; }

        public string NIT { get; set; }
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "El nombre comercial es requerido")]
        public string TradeName { get; set; }

        [Required(ErrorMessage = "El primer teléfono de contacto es requerido")]
        public string FirstPhoneNumber { get; set; }
        public string SecondPhoneNumber { get; set; }
        public string FirstLandLine { get; set; }
        public string SecondLandLine { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        public ClientAddressModel ClientAddress { get; set; }

        public List<ContactPersonModel> ContactPeople { get; set; }

        [Required(ErrorMessage = "El estado del cliente es requerido")]
        public ClientState State { get; set; }
    }
}
