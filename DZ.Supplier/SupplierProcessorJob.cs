using Microsoft.Extensions.Logging;

namespace DZ.SupplierProcessor
{
    public interface ISupplierProcessorJob
    {
        Task Run();
    }

    public class SupplierProcessorJob : ISupplierProcessorJob
    {
        private readonly ILogger<SupplierProcessorJob> _logger;

        public SupplierProcessorJob(ILogger<SupplierProcessorJob> logger)
        {
            _logger = logger;
        }

        public async Task Run()
        {
            _logger.LogInformation("Supplier processor job started");

            // TODO: do actual work
            await Task.Delay(30);

            _logger.LogInformation("Supplier processor job ended");
        }
    }
}
