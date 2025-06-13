using DZ.SupplierProcessor.Database.Mapping;
using DZ.SupplierProcessor.Dto;

namespace DZ.SupplierProcessor.Tests.Database
{
    [TestFixture]
    public class BoxMapperTest
    {
        [Test]
        public void MapToBox_MapsBasicProperties_ReturnsBox()
        {
            var dto = new BoxDto(
                supplierIdentifier: "TRSP117",
                boxIdentifier: "6874454I");

            var result = BoxMapper.MapToBox(dto);

            Assert.That(result.SupplierIdentifier, Is.EqualTo(dto.SupplierIdentifier));
            Assert.That(result.BoxIdentifier, Is.EqualTo(dto.BoxIdentifier));
            Assert.That(result.Contents, Is.Not.Null);
            Assert.That(result.Contents, Is.Empty);
            Assert.That(result.CreatedBy, Is.EqualTo("SUPPLIER_PROCESSOR"));
        }

        [Test]
        public void MapToBox_InputBoxWithProducts_ReturnsBox()
        {
            var dto = new BoxDto(
                supplierIdentifier: "TRSP117",
                boxIdentifier: "6874454I");

            dto.Products.AddRange(
                new ProductDto("P000001661", "9781465121550", "12"),
                new ProductDto("P000001662", "9781465121550", "12"));

            var result = BoxMapper.MapToBox(dto);

            Assert.That(result.SupplierIdentifier, Is.EqualTo(dto.SupplierIdentifier));
            Assert.That(result.BoxIdentifier, Is.EqualTo(dto.BoxIdentifier));
            Assert.That(result.Contents, Has.Count.EqualTo(2));
            Assert.That(result.Contents.First().PoNumber, Is.EqualTo("P000001661"));
            Assert.That(result.Contents.First().Quantity, Is.EqualTo(12));
            Assert.That(result.Contents.Last().PoNumber, Is.EqualTo("P000001662"));
            Assert.That(result.Contents.Last().Quantity, Is.EqualTo(12));
        }

        [Test]
        public void MapToBox_NullObjectInput_ReturnException()
        {
            Assert.Throws<NullReferenceException>(() => BoxMapper.MapToBox(null));
        }
    }
}
