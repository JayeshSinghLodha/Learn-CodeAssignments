using GeoBoundaries.Display;
using GeoBoundaries.Interfaces;
using GeoBoundaries.Services;
using GeoBoundaries.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var host = Host.CreateDefaultBuilder(args)
        .UseSerilog((context, services, config) => config
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext())
        .ConfigureServices((_, services) =>
        {
            services.AddHttpClient<IGeocodeService, GoogleGeocodeService>();
            services.AddSingleton<ILocationValidator, LocationValidator>();
        })
        .Build();

    var geocodeService = host.Services.GetRequiredService<IGeocodeService>();
    var validator      = host.Services.GetRequiredService<ILocationValidator>();
    var logger         = host.Services.GetRequiredService<ILogger<Program>>();

    ResultsDisplay.ShowBanner();

    while (true)
    {
        Console.Write("  Enter a location (or 'exit' to quit): ");
        var input = Console.ReadLine()?.Trim();

        if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            break;

        if (!validator.IsValid(input, out var errorMessage))
        {
            ResultsDisplay.ShowWarning(errorMessage);
            continue;
        }

        try
        {
            var results = await geocodeService.GetLocationsAsync(input!);
            ResultsDisplay.ShowResults(results, input!);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Network error contacting the geocoding API");
            ResultsDisplay.ShowError("Network error: could not reach the geocoding service.");
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Geocoding failed for input: {Input}", input);
            ResultsDisplay.ShowError(ex.Message);
        }
    }

    Console.WriteLine("\n  Goodbye!\n");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
