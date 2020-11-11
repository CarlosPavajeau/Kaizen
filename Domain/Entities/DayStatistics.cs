using System;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class DayStatistics : Statistics
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
