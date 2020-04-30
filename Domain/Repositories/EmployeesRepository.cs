using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public class EmployeesRepository : RepositoryBase<Employee, string>, IEmployeesRepository
    {
        public EmployeesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
