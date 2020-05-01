using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Domain.Repositories
{
	public interface IEmployeesRepository : IRepositoryBase<Employee, string>
	{
		IQueryable<EmployeeCharge> GetAllEmployeeCharges();
		Task<Employee> GetEmployeeWithCharge(string id);
	}
}
