using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public class ClientsRepository : RepositoryBase<Client, string>, IClientsRepository
    {
        public ClientsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
