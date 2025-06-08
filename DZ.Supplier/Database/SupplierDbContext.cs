using DZ.SupplierProcessor.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DZ.SupplierProcessor.Database
{
    public class SupplierDbContext : DbContext
    {
        public SupplierDbContext(DbContextOptions<SupplierDbContext> options)
            : base(options)
        {
        }

        public DbSet<Content> Contents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-NKNU84M\\SQLEXPRESS;Database=dz_supplier_database;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    public class SupplierDbContextFactory : IDesignTimeDbContextFactory<SupplierDbContext>
    {
        public SupplierDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SupplierDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-NKNU84M\\SQLEXPRESS;Database=dz_supplier_database;Trusted_Connection=True;TrustServerCertificate=True;");

            return new SupplierDbContext(optionsBuilder.Options);
        }
    }
}
