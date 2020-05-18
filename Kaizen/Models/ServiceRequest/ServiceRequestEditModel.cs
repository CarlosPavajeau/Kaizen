using System;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.ServiceRequest
{
    public class ServiceRequestEditModel
    {
        public DateTime Date { get; set; }
        public RequestState State { get; set; }
    }
}
