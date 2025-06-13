using DZ.SupplierProcessor.BackgroundJobs;
using DZ.SupplierProcessor.Database;
using DZ.SupplierProcessor.Dto;
using DZ.SupplierProcessor.FileProcessing;
using Microsoft.Extensions.Logging;
using Moq;

namespace DZ.SupplierProcessor.Tests.BackgroundJobs
{
    [TestFixture]
    public class SupplierProcessorJobTest
    {
        [Test]
        public async Task RunAsync_Successfull()
        {
            var loggerMock = new Mock<ILogger<SupplierProcessorJob>>();

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

            var fileProcessorMock = new Mock<IFileProcessor>(MockBehavior.Strict);
            fileProcessorMock.Setup(x => x.ProcessFile()).Returns(It.IsAny<List<BoxDto>>());

            var boxRepositoryMock = new Mock<IBoxRepository>(MockBehavior.Strict);

            var supplierProcessorJob = new SupplierProcessorJob(
                    loggerMock.Object,
                    fileProcessorMock.Object,
                    boxRepositoryMock.Object);

            await supplierProcessorJob.RunAsync();
        }
    }
}
