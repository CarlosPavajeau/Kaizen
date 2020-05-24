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
            IServicesRepository servicesRepository,
            IActivitiesRepository activitiesRepository,
            IServiceRequestsRepository serviceRequestsRepository,
            IWorkOrdersRepository workOrdersRepository
            )
        {
            _dbContext = applicationDbContext;
            ApplicationUsers = userRepository;
            Clients = clientsRepository;
            Employees = employeesRepository;
            Equipments = equipmentsRepository;
            Products = productsRepository;
            Services = servicesRepository;
            Activities = activitiesRepository;
            ServiceRequests = serviceRequestsRepository;
            WorkOrders = workOrdersRepository;
        }
        public IClientsRepository Clients { get; }

        public IApplicationUserRepository ApplicationUsers { get; }

        public IEmployeesRepository Employees { get; }

        public IEquipmentsRepository Equipments { get; }

        public IProductsRepository Products { get; }

        public IServicesRepository Services { get; }

        public IActivitiesRepository Activities { get; }

        public IServiceRequestsRepository ServiceRequests { get; }

        public IWorkOrdersRepository WorkOrders { get; }

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
