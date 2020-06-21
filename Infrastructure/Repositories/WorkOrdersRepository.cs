using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class WorkOrdersRepository : RepositoryBase<WorkOrder, int>, IWorkOrdersRepository
    {
        public WorkOrdersRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<Sector>> GetSectorsAsync()
        {
            return await ApplicationDbContext.Sectors.ToListAsync();
        }
    }
}
