using System.Linq;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IServicesRepository : IRepositoryBase<Service, string>
    {
        IQueryable<ServiceType> GetServiceTypes();
    }
}
