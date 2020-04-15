using Kaizen.EditModels;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.InputModels
{
    public class ApplicationUserInputModel : ApplicationUserEditModel
    {
        [Required]
        public string Username { get; set; }
    }
}
