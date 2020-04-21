using System;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class Equipment
    {
        [Key, MaxLength(20)]
        public string Code { get; set; }
        public DateTime MaintenanceDate { get; set; }
    }
}
