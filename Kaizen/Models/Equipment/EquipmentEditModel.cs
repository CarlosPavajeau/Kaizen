using System;

namespace Kaizen.Models.Equipment
{
    public class EquipmentEditModel
    {
        public string Name { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
