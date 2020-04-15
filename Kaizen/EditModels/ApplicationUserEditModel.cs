using System.ComponentModel.DataAnnotations;

namespace Kaizen.EditModels
{
    public class ApplicationUserEditModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
