using System.ComponentModel.DataAnnotations;

namespace DZ.SupplierProcessor.Database.Models
{
    abstract public class Base
    {
        [Required]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
