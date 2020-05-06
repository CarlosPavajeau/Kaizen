using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Repositories;

namespace Kaizen.Infrastructure.Repositories
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitWork(
            ApplicationDbContext applicationDbContext,
            IApplicationUserRepository userRepository,
            IClientsRepository clientsRepository,
            IEmployeesRepository employeesRepository,
            IEquipmentsRepository equipmentsRepository,
            IProductsRepository productsRepository,
            IServicesRepository servicesRepository
            )
        {
            _dbContext = applicationDbContext;
            ApplicationUsers = userRepository;
            Clients = clientsRepository;
            Employees = employeesRepository;
            Equipments = equipmentsRepository;
            Products = productsRepository;
            Services = servicesRepository;
        }
        public IClientsRepository Clients { get; }

        public IApplicationUserRepository ApplicationUsers { get; }

        public IEmployeesRepository Employees { get; }

        public IEquipmentsRepository Equipments { get; }

        public IProductsRepository Products { get; }

        public IServicesRepository Services { get; }

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
