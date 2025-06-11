using DZ.SupplierProcessor.Dto;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace DZ.SupplierProcessor.FileProcessing
{
    public interface IBoxProcessor
    {
        BoxDto CreateBox(string boxInputString, int lineNumber);
    }

    public class BoxProcessor : IBoxProcessor
    {
        private readonly ILogger<BoxProcessor> _logger;

        public BoxProcessor(ILogger<BoxProcessor> logger)
        {
            _logger = logger;
        }

        public BoxDto CreateBox(string boxInputString, int lineNumber)
        {
            if (string.IsNullOrEmpty(boxInputString))
            {
                _logger.LogError($"Couldn't parse string - {boxInputString}, line number - {lineNumber}");
                throw new ArgumentException($"Provided invalid string for box parsing - {boxInputString}, line number {lineNumber}");
            }

            string noSpacesBox = Regex.Replace(boxInputString.TrimEnd(), @"\s+", "-");
            string[] boxProperties = noSpacesBox.Split('-');

            if (boxProperties.Length < 3)
            {
                _logger.LogError($"Invalid box format: {boxInputString}, lineNumber {lineNumber}");
                throw new FormatException($"Invalid box format: {boxInputString}, lineNumber {lineNumber}");
            }

            return new BoxDto(boxProperties[1], boxProperties[2]);
        }
    }
}
