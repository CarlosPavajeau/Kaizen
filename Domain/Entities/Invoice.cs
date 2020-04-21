using System.ComponentModel.DataAnnotations;

namespace Kaizen.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
    }
}
