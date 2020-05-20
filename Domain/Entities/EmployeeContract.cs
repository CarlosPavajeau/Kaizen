using System;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class EmployeeContract
    {
        [Key, MaxLength(30)]
        public string ContractCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
