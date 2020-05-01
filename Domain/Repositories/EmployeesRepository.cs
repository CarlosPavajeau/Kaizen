using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
    public class EmployeesRepository : RepositoryBase<Employee, string>, IEmployeesRepository
    {
        public EmployeesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

		public IQueryable<EmployeeCharge> GetAllEmployeeCharges()
		{
            return ApplicationDbContext.EmployeeCharges;
		}

		public async Task<Employee> GetEmployeeWithCharge(string id)
		{
			return await ApplicationDbContext.Employees.Include(e => e.EmployeeCharge).Where(e => e.Id == id).FirstOrDefaultAsync();
		}
	}
}
