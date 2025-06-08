using DZ.SupplierProcessor.Database;
using Microsoft.EntityFrameworkCore;

namespace DZ.SupplierProcessor.Tests
{
    public static class DbContextHelper
    {
        public static SupplierDbContext CreateContext()
        {
            var _contextOptions = new DbContextOptionsBuilder<SupplierDbContext>()
                .UseInMemoryDatabase(databaseName: "in_memory_supplier_database")
                .Options;

            return new SupplierDbContext(_contextOptions);
        }
    }
}
