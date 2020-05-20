using System.Collections.Generic;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Models.Activity;

namespace Kaizen.Mappers
{
    public class ActivityMapperProfile : Profile
    {
        public ActivityMapperProfile()
        {
            CreateMap<ActivityEditModel, Activity>();
            CreateMap<ActivityInputModel, Activity>().AfterMap((activityModel, activity) =>
            {
                activity.ActivitiesServices = new List<ActivityService>();
                foreach (var serviceCode in activityModel.ServiceCodes)
                {
                    activity.ActivitiesServices.Add(new ActivityService
                    {
                        ServiceCode = serviceCode,
                        Activity = activity
                    });
                }

                activity.ActivitiesEmployees = new List<ActivityEmployee>();
                foreach (var employeeId in activityModel.EmployeeCodes)
                {
                    activity.ActivitiesEmployees.Add(new ActivityEmployee
                    {
                        EmployeeId = employeeId,
                        Activity = activity
                    });
                }
            });
            CreateMap<Activity, ActivityViewModel>();
        }
    }
}
