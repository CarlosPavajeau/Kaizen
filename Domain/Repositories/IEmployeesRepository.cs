using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IEmployeesRepository : IRepositoryBase<Employee, string>
    {
        IQueryable<EmployeeCharge> GetAllEmployeeCharges();
        Task<Employee> GetEmployeeWithCharge(string id);
        Task<IEnumerable<Employee>> GetTechniciansAvailable(DateTime date, string[] serviceCodes);

        Task<IEnumerable<Employee>> EmployeesWithContractCloseToExpiration();

        Task<bool> EmployeeContractAlreadyExists(string contractCode);
        void AddNewEmployeeContract(EmployeeContract contract);

        Task<IEnumerable<Employee>> GetTechnicians();
    }
}
