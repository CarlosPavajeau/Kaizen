using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Models.EditModels
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
