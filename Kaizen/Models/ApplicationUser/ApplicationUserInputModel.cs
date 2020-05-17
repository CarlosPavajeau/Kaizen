using System.ComponentModel.DataAnnotations;

namespace Kaizen.Models.ApplicationUser
{
    public class ApplicationUserInputModel : ApplicationUserEditModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
