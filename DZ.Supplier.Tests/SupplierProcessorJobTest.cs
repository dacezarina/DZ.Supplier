using DZ.SupplierProcessor;
using Microsoft.Extensions.Logging;
using Moq;

namespace DZ.Supplier.Tests
{
    [TestFixture]
    public class SupplierProcessorJobTest
    {
        [Test]
        public async Task RunTest()
        {
            var loggerMock = new Mock<ILogger<SupplierProcessorJob>>(MockBehavior.Strict);

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

            var supplierProcessorJob = new SupplierProcessorJob(loggerMock.Object);

            await supplierProcessorJob.Run();
        }
    }
}
