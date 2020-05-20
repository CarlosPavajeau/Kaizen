using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Kaizen.Infrastructure.Repositories
{
    public class ActivitiesRepository : RepositoryBase<Activity, int>, IActivitiesRepository
    {
        public ActivitiesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<Activity> FindByIdAsync(int id)
        {
            Activity activity = await ApplicationDbContext.Activities.Include(a => a.Client)
                .Include(a => a.ActivitiesServices)
                .ThenInclude(a => a.Service).Include(a => a.ActivitiesEmployees).ThenInclude(a => a.Employee)
                .Where(a => a.Code == id).FirstOrDefaultAsync();

            if (activity != null)
            {
                activity.Services = new List<Service>();
                foreach (ActivityService activityService in activity.ActivitiesServices)
                {
                    activity.Services.Add(activityService.Service);
                }

                activity.Employees = new List<Employee>();
                foreach (ActivityEmployee activityEmployee in activity.ActivitiesEmployees)
                {
                    activity.Employees.Add(activityEmployee.Employee);
                }
            }

            return activity;
        }
    }
}
