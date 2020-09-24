using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Client
{
    public class ClientAddressModel
    {
        [Required(ErrorMessage = "La ciudad es requerida")]
        public string City { get; set; }

        [Required(ErrorMessage = "El barrio es requerido")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "La calle es requerida")]
        public string Street { get; set; }
    }
}
