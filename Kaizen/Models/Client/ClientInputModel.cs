using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.Client
{
    public class ClientInputModel : ClientEditModel
    {
        [Required(ErrorMessage = "El número de identificación es requerido")]
        public string Id { get; set; }

        [Required(ErrorMessage = "El id de usuario es requerido")]
        public string UserId { get; set; }
    }
}
