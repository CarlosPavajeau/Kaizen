using Kaizen.Models.EditModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Models.InputModels
{
    public class ApplicationUserInputModel : ApplicationUserEditModel
    {
        public string Username { get; set; }
    }
}
