using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DZ.SupplierProcessor
{
    public interface IFileProcessor
    {
        void ProcessFile();
    }

    public class FileProcessor : IFileProcessor
    {
        private readonly ILogger<FileProcessor> _logger;
        private readonly IConfiguration _configuration;

        public FileProcessor(ILogger<FileProcessor> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void ProcessFile()
        {
            try
            {

                if (!File.Exists($"{_configuration["BaseDirectoryForMonitoring"]}data.txt"))
                {
                    _logger.LogInformation("File not found.");
                    return;
                }

                using (StreamReader sr = new StreamReader($"{_configuration["BaseDirectoryForMonitoring"]}data.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        _logger.LogInformation(line);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("The file could not be read:");
                _logger.LogError(e.Message);
            }
        }
    }
}
