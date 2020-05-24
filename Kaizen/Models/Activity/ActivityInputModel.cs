using System.Collections.Generic;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.Activity
{
    public class ActivityInputModel : ActivityEditModel
    {
        public string ClientId { get; set; }
        public PeriodicityType Periodicity { get; set; }

        public List<string> EmployeeCodes { get; set; }
        public List<string> ServiceCodes { get; set; }
    }
}
