using Axpo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerTradeApp;
using PowerTradeApp.Repository;
using PowerTradeApp.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PowerTradeApp.Helpers;
using PowerTradeApp.Interfaces;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, builder) =>
    {
        // Cargar la configuración desde appsettings.json
        builder.SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        
        services.AddSingleton<IPowerService, PowerService>();
        services.AddSingleton<IPowerServiceRepository, PowerServiceRepository>();
        services.AddSingleton<ICsvGenerator, CsvGenerator>();
        services.AddSingleton<IPowerTradeProcessor, PowerTradeProcessor>();
        services.AddSingleton<IAggregationService, AggregationService>();
       
        services.AddSingleton<IRetryPolicy>(provider =>
        {
            var appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;
            return new RetryPolicy(appSettings.MaxRetryAttempts, TimeSpan.FromMilliseconds(appSettings.RetryDelayMilliseconds));
        });

    })
    .Build();


var powerTradeProcessor = host.Services.GetRequiredService<IPowerTradeProcessor>();
var appSettings = host.Services.GetRequiredService<IConfiguration>().GetSection("AppSettings").Get<AppSettings>();


var dayAheadDate = DateTime.UtcNow.AddDays(1); 
await powerTradeProcessor.ProcessDayAheadTradesAsync(dayAheadDate, appSettings.CsvDirectory);

Console.WriteLine("El procesamiento de las transacciones se ha completado. Presiona cualquier tecla para salir.");
Console.ReadKey();