using DZ.SupplierProcessor.Database.Mapping;
using DZ.SupplierProcessor.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DZ.SupplierProcessor.Database
{
    public interface IBoxRepository
    {
        bool Save(List<BoxDto> listOfBoxes);
    }

    public class BoxRepository : IBoxRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BoxRepository> _logger;

        public BoxRepository(
            IConfiguration configuration,
            ILogger<BoxRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public bool Save(List<BoxDto> listOfBoxes)
        {
            try
            {
                using (var context = new SupplierDbContext())
                {
                    // not the most efficent way how to do it, 
                    // most probably have to rewrite to SQL query to get the performance
                    var boxes = listOfBoxes.Select(BoxMapper.MapToBox).ToList();
                    context.Boxes.AddRange(boxes);
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError("Failed save to into database");
                _logger.LogError(exception.Message);
                if (exception.InnerException != null)
                {
                    _logger.LogError(exception.InnerException.Message);
                }

                return false;
            }
        }
    }
}
