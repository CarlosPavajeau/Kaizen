using System.ComponentModel.DataAnnotations;
using Kaizen.Models.ApplicationUser;

namespace Kaizen.Models.Client
{
    public class ClientInputModel : ClientEditModel
    {
        [Required(ErrorMessage = "El número de identificación es requerido")]
        public string Id { get; set; }

        public ApplicationUserInputModel User { get; set; }
    }
}
