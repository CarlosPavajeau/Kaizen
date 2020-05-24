using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class Equipment
    {
        [Key, MaxLength(20)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime MaintenanceDate { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }

        public List<EquipmentService> EquipmentsServices { get; set; }
    }
}
