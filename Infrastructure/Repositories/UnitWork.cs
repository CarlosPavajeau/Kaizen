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

        public UnitWork(ApplicationDbContext applicationDbContext, IDomainEventDispatcher eventDispatcher)
        {
            _dbContext = applicationDbContext;
            _eventDispatcher = eventDispatcher;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _dbContext is not null)
            {
                _dbContext.Dispose();
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
                {
                    await _eventDispatcher.Dispatch(@event);
                }
            }
        }
    }
}
