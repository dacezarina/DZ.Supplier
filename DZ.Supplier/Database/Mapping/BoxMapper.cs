using DZ.SupplierProcessor.Database.Models;
using DZ.SupplierProcessor.Dto;

namespace DZ.SupplierProcessor.Database.Mapping
{
    // Can be used some external library for mapping but as project is small 
    // didn't any library for that
    public static class BoxMapper
    {
        public static Box MapToBox(BoxDto dto)
        {
            return new Box()
            {
                SupplierIdentifier = dto.SupplierIdentifier,
                BoxIdentifier = dto.BoxIdentifier,
                CreatedBy = "SUPPLIER_PROCESSOR",
                CreatedDate = DateTime.UtcNow,
                Contents = dto.Products.Select(ContentMapper.MapToContent).ToList()
            };
        }
    }
}
