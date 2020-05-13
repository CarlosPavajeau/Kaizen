using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Service
    {
        [Key, MaxLength(15)]
        public string Code { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        public int ServiceTypeId { get; set; }
        [ForeignKey("ServiceTypeId")]
        public ServiceType ServiceType { get; set; }
        public decimal Cost { get; set; }

        public List<EquipmentService> EquipmentsServices { get; set; }
        public List<ProductService> ProductsServices { get; set; }
        public List<EmployeeService> EmployeesServices { get; set; }
        public List<ServiceRequestService> ServiceRequestsServices { get; set; }

        [NotMapped]
        public List<Equipment> Equipments { get; set; }
        [NotMapped]
        public List<Product> Products { get; set; }
        [NotMapped]
        public List<Employee> Employees { get; set; }
    }
}
