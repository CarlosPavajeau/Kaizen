using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class WorkOrder : Entity
    {
        [Key]
        public int Code { get; set; }
        [ForeignKey("SectorId")]
        public Sector Sector { get; set; }
        public WorkOrderState WorkOrderState { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExecutionDate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Validity { get; set; }
        [MaxLength(500)]
        public string Observations { get; set; }

        public string ClientSignature { get; set; }

        public int ActivityCode { get; set; }
        public string EmployeeId { get; set; }
        public int SectorId { get; set; }

        [ForeignKey("ActivityCode")]
        public Activity Activity { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
