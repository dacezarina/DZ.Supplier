namespace DZ.SupplierProcessor.Dto
{
    public class Product
    {
        public Product(string poNumber, string isbn, string quantity)
        {
            PoNumber = poNumber;
            ISBN = isbn;
            Quantity = quantity;
        }

        public string PoNumber { get; set; }
        public string ISBN { get; set; }
        public string Quantity { get; set; }
    }
}
