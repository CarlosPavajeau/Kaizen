using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IActivitiesRepository : IRepositoryBase<Activity, int>
    {
        Task ScheduleActivities(Activity activity);
    }
}
