using System.ComponentModel.DataAnnotations;
using Kaizen.EditModels;

namespace Kaizen.InputModels
{
    public class ApplicationUserInputModel : ApplicationUserEditModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
