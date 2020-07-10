using System;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.ServiceRequest
{
    public class ServiceRequestEditModel
    {
        public DateTime Date { get; set; }
        public ServiceRequestState State { get; set; }
    }
}
