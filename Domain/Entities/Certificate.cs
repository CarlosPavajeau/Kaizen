using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Domain.Entities
{
    public class Certificate
    {
        [Key] public int Id { get; set; }

        [DataType(DataType.Date)] public DateTime Validity { get; set; }

        public int WorkOrderCode { get; set; }
        [ForeignKey("WorkOrderCode")] public WorkOrder WorkOrder { get; set; }
    }
}
