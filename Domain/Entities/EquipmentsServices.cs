using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class EquipmentsServices
    {
        [MaxLength(20)]
        public string EquipmentCode { get; set; }
        [MaxLength(15)]
        public string ServiceCode { get; set; }

        [ForeignKey("ServiceCode")]
        public Service Service { get; set; }
        [ForeignKey("EquipmentCode")]
        public Equipment Equipment { get; set; }
    }
}
