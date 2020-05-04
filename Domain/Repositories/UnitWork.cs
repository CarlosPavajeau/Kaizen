using System.Threading.Tasks;
using Kaizen.Domain.Data;

namespace Kaizen.Domain.Repositories
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitWork(
            ApplicationDbContext applicationDbContext,
            IApplicationUserRepository userRepository,
            IClientsRepository clientsRepository
            )
        {
            _dbContext = applicationDbContext;
            ApplicationUsers = userRepository;
            Clients = clientsRepository;
        }
        public IClientsRepository Clients { get; }

        public IApplicationUserRepository ApplicationUsers { get; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
