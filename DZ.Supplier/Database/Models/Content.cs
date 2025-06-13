namespace DZ.SupplierProcessor.Database.Models
{
    public class Content : Base
    {
        public string PoNumber { get; set; }
        public string Isbn { get; set; }
        public int Quantity { get; set; }

        // navigation properties for Entity Framework
        public int BoxId { get; set; }
        public Box Box { get; set; } = null!;
    }
}
