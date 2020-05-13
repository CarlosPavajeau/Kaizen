using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Activity : ServiceRequest
    {
        public List<ActivityEmployee> ActivitiesEmployees { get; set; }

        [NotMapped]
        public List<Employee> Employees { get; set; }
    }
}
