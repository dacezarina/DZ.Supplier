using DZ.SupplierProcessor.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DZ.SupplierProcessor.FileProcessing
{
    public interface IFileProcessor
    {
        List<BoxDto> ProcessFile();
    }

    public class FileProcessor : IFileProcessor
    {
        private readonly ILogger<FileProcessor> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBoxProcessor _boxProcessor;
        private readonly IProductProcessor _productProcessor;

        public const string BOX_LINE = "HDR";
        public const string PRODUCT_LINE = "LINE";

        public const string FILE_NAME = "data.txt";

        public FileProcessor(
            ILogger<FileProcessor> logger,
            IConfiguration configuration,
            IBoxProcessor boxProcessor,
            IProductProcessor productProcessor)
        {
            _logger = logger;
            _configuration = configuration;
            _boxProcessor = boxProcessor;
            _productProcessor = productProcessor;
        }

        public List<BoxDto> ProcessFile()
        {
            var boxes = new List<BoxDto>();

            try
            {
                if (!File.Exists($"{_configuration["BaseMonitoringDir"]}{FILE_NAME}"))
                {
                    _logger.LogInformation("File not found. Continue monitoring directory for file.");
                    return boxes;
                }

                using (StreamReader streamReader = new StreamReader($"{_configuration["BaseMonitoringDir"]}{FILE_NAME}"))
                {
                    string currentLine;
                    // adding line number
                    // in case if something goes wrong
                    // so it can be understand where the issue was
                    int lineNumber = 1;
                    BoxDto? currentBox = null;
                    while (streamReader != null && (currentLine = streamReader.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(currentLine))
                        {
                            lineNumber++;
                            continue;
                        }

                        if (currentLine.StartsWith(BOX_LINE))
                        {
                            currentBox = _boxProcessor.CreateBox(currentLine, lineNumber);
                            boxes.Add(currentBox);
                            lineNumber++;
                            continue;
                        }

                        if (currentBox != null && currentLine.StartsWith(PRODUCT_LINE))
                        {
                            var product = _productProcessor.CreateProduct(currentLine, lineNumber);
                            currentBox.Products.Add(product);
                            lineNumber++;
                            continue;
                        }
                    }
                }

                return boxes;
            }
            // TOTO: catch more specific exceptions
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return boxes;
            }
        }
    }
}
