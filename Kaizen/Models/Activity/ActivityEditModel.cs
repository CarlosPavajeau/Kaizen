using System;
using Kaizen.Domain.Entities;

namespace Kaizen.Models.Activity
{
    public class ActivityEditModel
    {
        public DateTime Date { get; set; }
        public ActivityState State { get; set; }
    }
}
