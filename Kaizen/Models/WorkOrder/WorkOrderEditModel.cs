using System;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.WorkOrder
{
    public class WorkOrderEditModel
    {
        public WorkOrderState WorkOrderState { get; set; }
        public DateTime DepatureTime { get; set; }
        public DateTime Validity { get; set; }
        public string ClientSignature { get; set; }
        public string Observations { get; set; }
    }
}
