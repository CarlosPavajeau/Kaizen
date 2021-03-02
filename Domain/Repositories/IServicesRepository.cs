using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IServicesRepository : IRepositoryBase<Service, string>
    {
        IQueryable<ServiceType> GetServiceTypes();
        Task<IEnumerable<ServiceType>> GetServiceTypesAsync();

        void Insert(ServiceType serviceType);
    }
}
