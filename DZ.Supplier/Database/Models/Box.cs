using System.ComponentModel.DataAnnotations;

namespace DZ.SupplierProcessor.Database.Models
{
    public class Box : Base
    {
        [Required]
        public string SupplierIdentifier { get; set; }

        [Required]
        public string BoxIdentifier { get; set; }

        public ICollection<Content> Contents { get; set; } = new List<Content>();
    }
}
