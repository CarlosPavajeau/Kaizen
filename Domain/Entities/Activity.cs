using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Activity : RequestBase
    {
        public List<ActivityEmployee> ActivitiesEmployees { get; set; }
        public List<ActivityService> ActivitiesServices { get; set; }

        [NotMapped]
        public List<Employee> Employees { get; set; }
        [NotMapped]
        public List<Service> Services { get; set; }
    }
}
