using DZ.SupplierProcessor.Database.Models;
using DZ.SupplierProcessor.Dto;

namespace DZ.SupplierProcessor.Database.Mapping
{
    // Can be used some external library for mapping but as project is small 
    // didn't any library for that
    public static class ContentMapper
    {
        public static Content MapToContent(ProductDto dto)
        {
            return new Content()
            {
                PoNumber =  dto.PoNumber,
                Isbn = dto.ISBN,
                // this is not save tryparse should be used instead, might fail 
                // as there could be mismatch in types
                Quantity = int.Parse(dto.Quantity),
                CreatedBy = "SUPPLIER_PROCESSOR",
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
