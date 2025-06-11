namespace DZ.SupplierProcessor.Dto
{
    public class Box
    {
        public Box(string supplierIdentifier, string boxIdentifier)
        {
            SupplierIdentifier = supplierIdentifier;
            BoxIdentifier = boxIdentifier;
        }
        public string SupplierIdentifier { get; set; }
        public string BoxIdentifier { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
