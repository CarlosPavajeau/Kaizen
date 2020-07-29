using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;

namespace Kaizen.Infrastructure.Repositories
{
    public class ServiceInvoicesRepository : RepositoryBase<ServiceInvoice, int>, IServiceInvoicesRepository
    {
        public ServiceInvoicesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
