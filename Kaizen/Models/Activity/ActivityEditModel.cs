using System;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.Activity
{
    public class ActivityEditModel
    {
        public DateTime Date { get; set; }
        public RequestState State { get; set; }
    }
}
