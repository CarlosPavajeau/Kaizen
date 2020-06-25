using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Kaizen.Domain.Entities
{
    public class Activity : RequestBase, ICloneable
    {
        public List<ActivityEmployee> ActivitiesEmployees { get; set; }
        public List<ActivityService> ActivitiesServices { get; set; }

        [NotMapped]
        public List<Employee> Employees { get; set; }
        [NotMapped]
        public List<Service> Services { get; set; }

        public object Clone()
        {
            Activity activity = new Activity
            {
                Date = Date,
                Client = Client,
                ClientId = ClientId,
                State = State,
                Periodicity = Periodicity,
                ActivitiesEmployees = new List<ActivityEmployee>(ActivitiesEmployees.Select(a => new ActivityEmployee { EmployeeId = a.EmployeeId })),
                ActivitiesServices = new List<ActivityService>(ActivitiesServices.Select(a => new ActivityService { ServiceCode = a.ServiceCode }))
            };

            activity.ActivitiesEmployees.ForEach(a => a.Activity = activity);
            activity.ActivitiesServices.ForEach(a => a.Activity = activity);

            return activity;
        }
    }
}
