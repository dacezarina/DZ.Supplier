using DZ.SupplierProcessor.FileProcessing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace DZ.SupplierProcessor.Tests.FileProcessing
{
    [TestFixture]
    public class FileProcessorTest
    {
        public FileProcessor fileProcessor;

        [SetUp]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<FileProcessor>>();

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

            var configurationMock = new Mock<IConfiguration>();
            var boxProcessorMock = new Mock<IBoxProcessor>();
            var productProcessorMock = new Mock<IProductProcessor>();

            fileProcessor = new FileProcessor(
                    loggerMock.Object,
                    configurationMock.Object,
                    boxProcessorMock.Object,
                    productProcessorMock.Object);
        }

        [Test]
        public void ProcessFile_Succesfull()
        {
            var result = fileProcessor.ProcessFile();
        }
    }
}
