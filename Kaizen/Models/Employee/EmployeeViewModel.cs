using Kaizen.Models.ApplicationUser;

namespace Kaizen.Models.Employee
{
    public class EmployeeViewModel : EmployeeEditModel
    {
        public string Id { get; set; }
        public EmployeeChargeViewModel EmployeeCharge { get; set; }
        public ApplicationUserViewModel User { get; set; }
    }
}
