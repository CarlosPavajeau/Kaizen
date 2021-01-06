using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IProductInvoicesRepository : IRepositoryBase<ProductInvoice, int>
    {
        new Task Insert(ProductInvoice entity);

        Task<IEnumerable<ProductInvoice>> GetPendingExpiredProductInvoices();

        Task<IEnumerable<ProductInvoice>> GetClientInvoices(string clientId);
    }
}
