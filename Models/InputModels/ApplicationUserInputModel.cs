using Kaizen.Models.EditModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Models.InputModels
{
    public class ApplicationUserInputModel : ApplicationUserEditModel
    {
        [Required]
        public string Username { get; set; }
    }
}
