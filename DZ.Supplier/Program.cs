using DZ.SupplierProcessor;
using Hangfire;
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

// register all services
services.AddScoped<ISupplierProcessorJob, SupplierProcessorJob>();

var serviceProvider = services.BuildServiceProvider();

// Currently use only in memory storage but in future 
// SQL server storage can be used as Hangfire supports alo SQL server,
// so information will not be lost 
// when service is restarted
GlobalConfiguration.Configuration
    .UseInMemoryStorage()
    .UseActivator(new DependencyJobActivator(serviceProvider));

using (var server = new BackgroundJobServer())
{
    RecurringJob.AddOrUpdate<ISupplierProcessorJob>(
        nameof(ISupplierProcessorJob),
        job => job.Run(),
        // TODO: move to config files, frequency can be changed if needed
        Cron.Minutely);

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}


