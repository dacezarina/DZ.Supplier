using DZ.SupplierProcessor.Database;
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
        private readonly IBoxRepository _boxRepository;

        public SupplierProcessorJob(
            ILogger<SupplierProcessorJob> logger,
            IFileProcessor fileProcessor,
            IBoxRepository boxRepository)
        {
            _logger = logger;
            _fileProcessor = fileProcessor;
            _boxRepository = boxRepository;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Supplier processor job started");

            var listofBoxes = _fileProcessor.ProcessFile();

            if (listofBoxes != null && listofBoxes.Count() > 0)
            {
                _boxRepository.Save(listofBoxes);
            }

            _logger.LogInformation("Supplier processor job ended");
        }
    }
}
