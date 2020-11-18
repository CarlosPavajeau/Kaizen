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
                .Include(e => e.EmployeeContract).Include(e => e.EmployeeCharge)
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
            IEnumerable<string> occupiedEmployeesCodes = await GetOccupiedEmployeesCodes(date);

            return await GetAll().Include(e => e.EmployeesServices)
                .Where(e => ((occupiedEmployeesCodes.Any()) || !occupiedEmployeesCodes.Contains(e.Id)) &&
                    TECHNICAL_EMPLOYEE_JOB_CODES.Contains(e.ChargeId) &&
                    e.EmployeesServices.Any(es => serviceCodes.Contains(es.ServiceCode)))
                .ToListAsync();
        }

        private async Task<IEnumerable<string>> GetOccupiedEmployeesCodes(DateTime date)
        {
            return await GetAll().Include(e => e.EmployeesActivities).ThenInclude(e => e.Activity)
                .Where(e => e.EmployeesActivities.Any(e => e.Activity.Date == date && e.Activity.State == ActivityState.Pending))
                .Select(e => e.Id).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<Employee>> EmployeesWithContractCloseToExpiration()
        {
            DateTime today = DateTime.Now;
            return await GetAll().Include(e => e.EmployeeContract).Include(e => e.User)
                .Where(e => (e.EmployeeContract.EndDate - today).Days == 3)
                .ToListAsync();
        }

        public async Task<bool> EmployeeContractAlreadyExists(string contractCode)
        {
            EmployeeContract contract = await ApplicationDbContext.Set<EmployeeContract>()
                .FindAsync(contractCode);
            return contract != null;
        }

        public void AddNewEmployeeContract(EmployeeContract contract)
        {
            ApplicationDbContext.Set<EmployeeContract>().Add(contract);
        }
    }
}
