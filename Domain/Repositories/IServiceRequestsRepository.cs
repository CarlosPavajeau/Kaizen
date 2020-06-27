using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IServiceRequestsRepository : IRepositoryBase<ServiceRequest, int>
    {
        Task<ServiceRequest> GetPendingCustomerServiceRequest(string clientId);
    }
}
