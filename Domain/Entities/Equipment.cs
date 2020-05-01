using System;
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
    }
}
