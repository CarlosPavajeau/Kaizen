using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
	public class ProductsRepository : RepositoryBase<Product, string>, IProductsRepository
	{
		public ProductsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}
	}
}
