using Kaizen.Domain.Entities;
using Kaizen.Models.Base;

namespace Kaizen.Models.Employee
{
    public class EmployeeEditModel : PersonEditModel
    {
        public int ChargeId { get; set; }
        public EmployeeContractModel EmployeeContract { get; set; }
        public EmployeeState State { get; set; }
    }
}
