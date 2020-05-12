using System;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class WorkOrder : Activity
    {
        [Required, MaxLength(40)]
        public string Sector { get; set; }
        public WorkOrderState WorkOrderState { get; set; }

        public DateTime ExecutionDate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepatureTime { get; set; }
        public DateTime Validity { get; set; }
        [MaxLength(500)]
        public string Observations { get; set; }
    }
}
