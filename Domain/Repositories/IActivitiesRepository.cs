using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IActivitiesRepository : IRepositoryBase<Activity, int>
    {
        Task ScheduleActivities(Activity activity);
        Task<IEnumerable<Activity>> GetPendingEmployeeActivities(string employeeId, DateTime date);

        Task<IEnumerable<Activity>> GetPendingClientActivities(string clientId);
        Task<IEnumerable<Activity>> GetAppliedClientActivities(string clinetId);
    }
}
