using System;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Data;
using Kaizen.Domain.Repositories;

namespace Kaizen.Infrastructure.Repositories
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDomainEventDispatcher _eventDispatcher;

        public UnitWork(
            ApplicationDbContext applicationDbContext,
            IDomainEventDispatcher eventDispatcher,
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
            _eventDispatcher = eventDispatcher;
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
            await DispatchDomainEvents();
        }

        private async Task DispatchDomainEvents()
        {
            IEntity[] domainEventEntities = _dbContext.ChangeTracker.Entries<IEntity>()
                .Select(p => p.Entity)
                .Where(p => p.DomainEvents.Any())
                .ToArray();

            foreach (IEntity entity in domainEventEntities)
            {
                while (entity.DomainEvents.TryTake(out IDomainEvent @event))
                    await _eventDispatcher.Dispatch(@event);
            }
        }
    }
}
