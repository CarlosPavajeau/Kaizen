using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;

namespace Kaizen.Infrastructure.Repositories
{
    public class ProductInvoicesRepository : RepositoryBase<ProductInvoice, int>, IProductInvoicesRepository
    {
        public ProductInvoicesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
