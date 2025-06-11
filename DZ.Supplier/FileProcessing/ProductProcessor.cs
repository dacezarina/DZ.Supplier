using DZ.SupplierProcessor.Dto;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace DZ.SupplierProcessor.FileProcessing
{
    public interface IProductProcessor
    {
        Product CreateProduct(string productString, int lineNumber);
    }

    public class ProductProcessor : IProductProcessor
    {
        private readonly ILogger<ProductProcessor> _logger;

        public ProductProcessor(ILogger<ProductProcessor> logger)
        {
            _logger = logger;
        }

        public Product CreateProduct(string productString, int lineNumber)
        {
            if (string.IsNullOrEmpty(productString))
            {
                _logger.LogError($"Provided invalid string for product parsing - {productString}, line number {lineNumber}");
                throw new ArgumentException($"Provided invalid string for product parsing - {productString}, line number {lineNumber}");
            }

            string productsWithoutSpaces = Regex.Replace(productString.TrimEnd(), @"\s+", "-");
            string[] productProperties = productsWithoutSpaces.Split('-');

            if (productProperties.Length < 4)
            {
                _logger.LogError($"Invalid box format: {productProperties}, lineNumber {lineNumber}");
                throw new FormatException($"Invalid box format: {productProperties}, lineNumber {lineNumber}");
            }

            return new Product(
                productProperties[1],
                productProperties[2],
                productProperties[3]);
        }
    }
}
