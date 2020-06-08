using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class EmployeesRepository : RepositoryBase<Employee, string>, IEmployeesRepository
    {
        public EmployeesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<Employee> FindByIdAsync(string id)
        {
            return await ApplicationDbContext.Employees
                .Where(e => e.Id == id || e.UserId == id).FirstOrDefaultAsync();
        }

        public IQueryable<EmployeeCharge> GetAllEmployeeCharges()
        {
            return ApplicationDbContext.EmployeeCharges;
        }

        public async Task<Employee> GetEmployeeWithCharge(string id)
        {
            return await ApplicationDbContext.Employees.Include(e => e.EmployeeCharge)
                .Where(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}
