using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;

namespace Kaizen.Infrastructure.Repositories
{
    public class ServiceRequestsRepository : RepositoryBase<ServiceRequest, int>, IServiceRequestsRepository
    {
        public ServiceRequestsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
