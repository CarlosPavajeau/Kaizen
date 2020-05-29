using System.Collections.Generic;

namespace Kaizen.Models.Service
{
    public class ServiceEditModel
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int ServiceTypeId { get; set; }

        public List<string> EquipmentCodes { get; set; }
        public List<string> ProductCodes { get; set; }
        public List<string> EmployeeCodes { get; set; }
    }
}
