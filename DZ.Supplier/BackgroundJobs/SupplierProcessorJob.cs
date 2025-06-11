using DZ.SupplierProcessor.FileProcessing;
using Microsoft.Extensions.Logging;

namespace DZ.SupplierProcessor.BackgroundJobs
{
    public interface ISupplierProcessorJob
    {
        Task RunAsync();
    }

    public class SupplierProcessorJob : ISupplierProcessorJob
    {
        private readonly ILogger<SupplierProcessorJob> _logger;
        private readonly IFileProcessor _fileProcessor;

        public SupplierProcessorJob(
            ILogger<SupplierProcessorJob> logger,
            IFileProcessor fileProcessor)
        {
            _logger = logger;
            _fileProcessor = fileProcessor;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Supplier processor job started");

            _fileProcessor.ProcessFile();

            _logger.LogInformation("Supplier processor job ended");
        }
    }
}
