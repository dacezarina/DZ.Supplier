using DZ.SupplierProcessor;
using DZ.SupplierProcessor.BackgroundJobs;
using DZ.SupplierProcessor.Database;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();

services.AddLogging(loggingBuilder =>
{
    // In production log files should be used,
    // for development purposes showing all errors in console
    loggingBuilder.AddSimpleConsole(options =>
    {
        options.IncludeScopes = true;
        options.SingleLine = true;
        options.TimestampFormat = "HH:mm:ss ";
    });
});

var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

services.AddSingleton<IConfiguration>(configuration);

//TODO: move to configuration
services.AddDbContext<SupplierDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-NKNU84M\\SQLEXPRESS;Database=dz_supplier_database;Trusted_Connection=True;TrustServerCertificate=True;"));

services.AddScoped<IFileProcessor, FileProcessor>();
services.AddScoped<ISupplierProcessorJob, SupplierProcessorJob>();

var serviceProvider = services.BuildServiceProvider();

// Currently use only in memory storage but in future 
// SQL server storage can be used as Hangfire supports alo SQL server,
// so information will not be lost 
// when service is restarted
GlobalConfiguration.Configuration
    .UseInMemoryStorage()
    .UseActivator(new DependencyJobActivator(serviceProvider));

// apply all migrations automotically when application is launched, before jobs are launched
using (var scope = serviceProvider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SupplierDbContext>();
    await db.Database.MigrateAsync();
}

using (var server = new BackgroundJobServer())
{
    RecurringJob.AddOrUpdate<ISupplierProcessorJob>(
        nameof(ISupplierProcessorJob),
        job => job.RunAsync(),
        // TODO: move to config files, frequency can be changed if needed
        Cron.Minutely);

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}


