using System.Linq;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public class ServicesRepository : RepositoryBase<Service, string>, IServicesRepository
    {
        public ServicesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public IQueryable<ServiceType> GetServiceTypes()
        {
            return ApplicationDbContext.ServiceTypes;
        }
    }
}
