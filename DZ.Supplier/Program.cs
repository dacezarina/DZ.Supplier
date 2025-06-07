using DZ.SupplierProcessor;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();

services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSimpleConsole(options =>
    {
        options.IncludeScopes = true;
        options.SingleLine = true;
        options.TimestampFormat = "HH:mm:ss ";
    });
});

services.AddScoped<ISupplierProcessorJob, SupplierProcessorJob>();

var serviceProvider = services.BuildServiceProvider();

GlobalConfiguration.Configuration
    .UseInMemoryStorage()
    .UseActivator(new DependencyJobActivator(serviceProvider));

using (var server = new BackgroundJobServer())
{
    RecurringJob.AddOrUpdate<ISupplierProcessorJob>(
        nameof(ISupplierProcessorJob),
        job => job.Run(),
        // TODO: move to config files
        Cron.Minutely);

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}


