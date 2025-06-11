namespace DZ.SupplierProcessor.Dto
{
    public class BoxDto
    {
        public BoxDto(string supplierIdentifier, string boxIdentifier)
        {
            SupplierIdentifier = supplierIdentifier;
            BoxIdentifier = boxIdentifier;
        }
        public string SupplierIdentifier { get; set; }
        public string BoxIdentifier { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
