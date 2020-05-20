using System;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.WorkOrder
{
    public class WorkOrderEditModel
    {
        public WorkOrderState WorkOrderState { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepatureTime { get; set; }
        public string Observations { get; set; }
    }
}
