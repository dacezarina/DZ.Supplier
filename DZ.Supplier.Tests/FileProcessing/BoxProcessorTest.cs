using DZ.SupplierProcessor.FileProcessing;
using Microsoft.Extensions.Logging;
using Moq;

namespace DZ.SupplierProcessor.Tests.FileProcessing
{
    [TestFixture]
    public class BoxProcessorTest
    {
        private BoxProcessor processor;

        [SetUp]
        public void SetUp()
        {
            //TODO: fix add mock behaviour strict
            var loggerMock = new Mock<ILogger<BoxProcessor>>();

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

            processor = new BoxProcessor(loggerMock.Object);
        }

        [Test]
        public void CreateBox_ValidInput_ReturnsCorrectBox()
        {
            string input = "HDR  TRSP117                                           6874454I";
            int lineNumber = 1;

            var result = processor.CreateBox(input, lineNumber);

            Assert.That(result.SupplierIdentifier, Is.EqualTo("TRSP117"));
            Assert.That(result.BoxIdentifier, Is.EqualTo("6874454I"));
        }

        [Test]
        public void CreateBox_EmptyInput_ThrowsArgumentException()
        {
            string input = "";
            int lineNumber = 1;

            var ex = Assert.Throws<ArgumentException>(() => processor.CreateBox(input, lineNumber));
            Assert.That(ex.Message, Contains.Substring("Provided invalid string for box parsing"));
        }

        [Test]
        public void CreateBox_NullInput_ThrowsArgumentException()
        {
            string input = null;
            int lineNumber = 1;

            var ex = Assert.Throws<ArgumentException>(() => processor.CreateBox(input, lineNumber));
            Assert.That(ex.Message, Contains.Substring("Provided invalid string for box parsing"));
        }

        [Test]
        public void CreateBox_InvalidFormat_ThrowsFormatException()
        {
            string input = "HDR  TRSP117";
            int lineNumber = 1;

            var ex = Assert.Throws<FormatException>(() => processor.CreateBox(input, lineNumber));
            Assert.That(ex.Message, Contains.Substring("Invalid box format"));
        }

        [Test]
        public void CreateBox_MultipleSpaces_ReturnsCorrectBox()
        {
            string input = "HDR  TRSP117      6874454I";
            int lineNumber = 1;

            var result = processor.CreateBox(input, lineNumber);

            Assert.That(result.SupplierIdentifier, Is.EqualTo("TRSP117"));
            Assert.That(result.BoxIdentifier, Is.EqualTo("6874454I"));
        }

        [Test]
        public void CreateBox_TabsInsteadOfSpaces_ReturnsCorrectBox()
        {
            string input = "HDR\tTRSP117\t\t\t6874454I";
            int lineNumber = 1;

            var result = processor.CreateBox(input, lineNumber);

            Assert.That(result.SupplierIdentifier, Is.EqualTo("TRSP117"));
            Assert.That(result.BoxIdentifier, Is.EqualTo("6874454I"));
        }

    }
}
