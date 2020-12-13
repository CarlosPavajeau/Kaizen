using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class ServicesRepository : RepositoryBase<Service, string>, IServicesRepository
    {
        public ServicesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<Service> FindByIdAsync(string id)
        {
            return await ApplicationDbContext.Services
                .Include(s => s.ServiceType)
                .Include(s => s.EmployeesServices).ThenInclude(s => s.Employee)
                .Include(s => s.EquipmentsServices).ThenInclude(s => s.Equipment)
                .Include(s => s.ProductsServices).ThenInclude(s => s.Product)
                .FirstOrDefaultAsync(s => s.Code == id);
        }

        public IQueryable<ServiceType> GetServiceTypes()
        {
            return ApplicationDbContext.ServiceTypes;
        }

        public async Task<IEnumerable<ServiceType>> GetServiceTypesAsync()
        {
            return await ApplicationDbContext.ServiceTypes.ToListAsync();
        }
    }
}
