using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public class EquipmentsRepository : RepositoryBase<Equipment, string>, IEquipmentsRepository
    {
        public EquipmentsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
