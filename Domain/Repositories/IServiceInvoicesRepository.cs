using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IServiceInvoicesRepository : IRepositoryBase<ServiceInvoice, int>
    {
        Task<IEnumerable<ServiceInvoice>> GetClientInvoices(string clientId);
    }
}
