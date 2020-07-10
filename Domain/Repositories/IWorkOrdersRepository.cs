using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IWorkOrdersRepository : IRepositoryBase<WorkOrder, int>
    {
        Task<IEnumerable<Sector>> GetSectorsAsync();

        Task<WorkOrder> FindByActivityCodeAsync(int activityCode);
    }
}
