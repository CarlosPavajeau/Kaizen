using System.Collections.Generic;

namespace Kaizen.Models.Activity
{
    public class ActivityInputModel : ActivityEditModel
    {
        public string ClientId { get; set; }

        public List<string> EmployeeCodes { get; set; }
        public List<string> ServiceCodes { get; set; }
    }
}
