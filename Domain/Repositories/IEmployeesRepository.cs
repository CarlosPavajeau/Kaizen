using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Repositories
{
    public interface IEmployeesRepository : IRepositoryBase<Employee, string>
    {
    }
}