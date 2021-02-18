using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class ClientsRepository : RepositoryBase<Client, string>, IClientsRepository
    {
        public ClientsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<Client> FindByIdAsync(string id)
        {
            return await ApplicationDbContext.Clients.Include(c => c.ClientAddress)
                .Include(c => c.ContactPeople)
                .FirstOrDefaultAsync(c => c.Id == id || c.UserId == id);
        }

        public async Task<Client> GetClientWithUser(string id)
        {
            return await ApplicationDbContext.Clients.Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<string> GetClientId(string userId)
        {
            return await ApplicationDbContext.Clients.Where(c => c.UserId == userId)
                .Select(c => c.Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Client>> GetClientRequestsAsync()
        {
            return await ApplicationDbContext.Clients
                .Include(c => c.ClientAddress)
                .Include(c => c.ContactPeople)
                .Where(c => c.State == ClientState.Pending).ToListAsync();
        }
    }
}
