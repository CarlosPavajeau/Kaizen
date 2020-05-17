using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;

namespace Kaizen.Infrastructure.Repositories
{
    public class ActivitiesRepository : RepositoryBase<Activity, int>, IActivitiesRepository
    {
        public ActivitiesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
