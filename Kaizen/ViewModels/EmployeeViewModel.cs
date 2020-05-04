using Kaizen.InputModels;

namespace Kaizen.ViewModels
{
    public class EmployeeViewModel : EmployeeInputModel
    {
        public EmployeeChargeViewModel EmployeeCharge { get; set; }
    }
}
