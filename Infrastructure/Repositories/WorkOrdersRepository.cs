using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;

namespace Kaizen.Infrastructure.Repositories
{
    public class WorkOrdersRepository : RepositoryBase<WorkOrder, int>, IWorkOrdersRepository
    {
        public WorkOrdersRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
