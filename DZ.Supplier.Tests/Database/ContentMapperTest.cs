using DZ.SupplierProcessor.Database.Mapping;
using DZ.SupplierProcessor.Dto;

namespace DZ.SupplierProcessor.Tests.Database
{
    [TestFixture]
    public class ContentMapperTest
    {
        [Test]
        public void MapToContent_ReturnsValidContent()
        {
            var dto = new ProductDto("G000009810", "9781473662179", "12");

            var result = ContentMapper.MapToContent(dto);

            Assert.That(result.PoNumber, Is.EqualTo(dto.PoNumber));
            Assert.That(result.Isbn, Is.EqualTo(dto.ISBN));
            Assert.That(result.Quantity, Is.EqualTo(12));
            Assert.That(result.CreatedBy, Is.EqualTo("SUPPLIER_PROCESSOR"));
        }

        [Test]
        public void MapToContent_WhenIsbnIsNull_ReturnsValidContent()
        {
            var dto = new ProductDto("G000009810", null, "12");

            var result = ContentMapper.MapToContent(dto);

            Assert.That(result.PoNumber, Is.EqualTo(dto.PoNumber));
            Assert.That(result.Isbn, Is.EqualTo(dto.ISBN));
            Assert.That(result.Quantity, Is.EqualTo(12));
            Assert.That(result.CreatedBy, Is.EqualTo("SUPPLIER_PROCESSOR"));
        }

        [Test]
        public void MapToContent_NotValidQuantity_ReturnsFormatException()
        {
            var dto = new ProductDto("G000009810", "9781473662179", "not_a_number");

            Assert.Throws<FormatException>(() => ContentMapper.MapToContent(dto));
        }

        [Test]
        public void MapToContent_InputQuantityTooLarge_ReturnsOverFlowException()
        {
            var dto = new ProductDto("G000009810", "9781473662179", "99999999999999999999");

            Assert.Throws<OverflowException>(() => ContentMapper.MapToContent(dto));
        }

        [Test]
        public void MapToContent_InputQuantityWithSpaces_ReturnsValidContent()
        {
            var dto = new ProductDto("G000009810", "9781473662179", "  15  ");

            var result = ContentMapper.MapToContent(dto);

            Assert.That(15, Is.EqualTo(result.Quantity));
        }
    }
}
