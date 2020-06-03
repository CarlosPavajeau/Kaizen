using System.Collections.Generic;
using Kaizen.Models.Employee;
using Kaizen.Models.Equipment;
using Kaizen.Models.Product;

namespace Kaizen.Models.Service
{
    public class ServiceViewModel : ServiceInputModel
    {
        public ServiceTypeViewModel ServiceType { get; set; }

        public List<ProductViewModel> Products { get; set; }
        public List<EquipmentViewModel> Equipments { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
    }
}
