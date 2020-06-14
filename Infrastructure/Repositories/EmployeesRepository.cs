using System;
using System.Collections.Generic;
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
        readonly int[] TECHNICAL_EMPLOYEE_JOB_CODES = new[] { 6, 7, 8 };
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

        public async Task<IEnumerable<Employee>> GetTechniciansAvailable(DateTime date, string[] serviceCodes)
        {
            List<IEnumerable<Employee>> activities = await ApplicationDbContext.Activities
                .Where(a => a.Date == date && a.State == RequestState.Pending)
                .Include(a => a.ActivitiesEmployees).ThenInclude(ac => ac.Employee)
                .Select(a => a.ActivitiesEmployees.Select(ac => ac.Employee)).ToListAsync();

            HashSet<string> employeesCodes = new HashSet<string>();

            activities.ForEach(ac =>
            {
                ac.ToList().ForEach(e =>
                {
                    employeesCodes.Add(e.Id);
                });
            });

            List<Employee> techniciansAvailable = await GetAll().Include(e => e.EmployeeCharge).Include(e => e.EmployeesServices)
                .Where(e => !employeesCodes.Contains(e.Id) &&
                       TECHNICAL_EMPLOYEE_JOB_CODES.Contains(e.EmployeeCharge.Id) &&
                       e.EmployeesServices.Any(es => serviceCodes.Contains(es.ServiceCode))
                       )
                .ToListAsync();

            return techniciansAvailable;
        }
    }
}
