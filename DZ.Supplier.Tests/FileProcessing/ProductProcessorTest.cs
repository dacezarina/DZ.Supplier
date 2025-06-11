using DZ.SupplierProcessor.FileProcessing;
using Microsoft.Extensions.Logging;
using DZ.SupplierProcessor.Dto;
using Moq;

namespace DZ.SupplierProcessor.Tests.FileProcessing
{
    [TestFixture]
    public class ProductProcessorTest
    {
        private ProductProcessor processor;

        [SetUp]
        public void SetUp()
        {
            //TODO: fix add mock behaviour strict
            var loggerMock = new Mock<ILogger<ProductProcessor>>();

            loggerMock.Setup(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception?, string>>()
            ));

            loggerMock.Setup(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ));

            loggerMock.Setup(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ));

            processor = new ProductProcessor(loggerMock.Object);
        }

        [Test]
        public void CreateProduct_ValidInput_ReturnsCorrectProduct()
        {
            string input = "LINE P000001661         9781465121550         12";
            int lineNumber = 2;

            var result = processor.CreateProduct(input, lineNumber);

            Assert.That(result.PoNumber, Is.EqualTo("P000001661"));
            Assert.That(result.ISBN, Is.EqualTo("9781465121550"));
            Assert.That(result.Quantity, Is.EqualTo("12"));
        }

        [Test]
        public void CreateProduct_EmptyInput_ThrowsArgumentException()
        {
            string input = "";
            int lineNumber = 2;

            var ex = Assert.Throws<ArgumentException>(() => processor.CreateProduct(input, lineNumber));
            Assert.That(ex.Message, Does.Contain("Provided invalid string for product parsing"));
        }

        [Test]
        public void CreateProduct_NullInput_ThrowsArgumentException()
        {
            string input = null;
            int lineNumber = 2;

            var ex = Assert.Throws<ArgumentException>(() => processor.CreateProduct(input, lineNumber));
            Assert.That(ex.Message, Does.Contain("Provided invalid string for product parsing"));
        }

        [Test]
        public void CreateProduct_InsufficientParts_ThrowsFormatException()
        {
            string input = "LINE P000001661         9781465121550";
            int lineNumber = 2;

            var ex = Assert.Throws<FormatException>(() => processor.CreateProduct(input, lineNumber));
            Assert.That(ex.Message, Does.Contain("Invalid box format"));
        }

        [Test]
        public void CreateProduct_MultipleSpaces_ReturnsCorrectProduct()
        {
            string input = "LINE  P000001661  9781465121550  12";
            int lineNumber = 2;

            var result = processor.CreateProduct(input, lineNumber);

            Assert.That(result.PoNumber, Is.EqualTo("P000001661"));
            Assert.That(result.ISBN, Is.EqualTo("9781465121550"));
            Assert.That(result.Quantity, Is.EqualTo("12"));
        }

        [Test]
        public void CreateProduct_TabsInsteadOfSpaces_ReturnsCorrectProduct()
        {
            string input = "LINE\tP000001661\t9781465121550\t12";
            int lineNumber = 2;

            var result = processor.CreateProduct(input, lineNumber);

            Assert.That(result.PoNumber, Is.EqualTo("P000001661"));
            Assert.That(result.ISBN, Is.EqualTo("9781465121550"));
            Assert.That(result.Quantity, Is.EqualTo("12"));
        }

        [Test]
        public void CreateProduct_ExtraSpacesAtEnd_ReturnsCorrectProduct()
        {
            string input = "LINE P000001661 9781465121550 12      ";
            int lineNumber = 2;

            var result = processor.CreateProduct(input, lineNumber);

            Assert.That(result.PoNumber, Is.EqualTo("P000001661"));
            Assert.That(result.ISBN, Is.EqualTo("9781465121550"));
            Assert.That(result.Quantity, Is.EqualTo("12"));
        }

        [Test]
        public void CreateProduct_WithDifferentQuantityFormat_ReturnsCorrectProduct()
        {
            string input = "LINE P000001661 9781465121550 0012";
            int lineNumber = 2;

            var result = processor.CreateProduct(input, lineNumber);

            Assert.That(result.Quantity, Is.EqualTo("0012"));
        }
    }
}
