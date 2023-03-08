using WeatherForecastExample.Blazor.Server.ApplicationCore.Clients;
using WeatherForecastExample.Blazor.Server.ApplicationCore.Services;

namespace WeatherForecastExample.Blazor.Server.Web;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IGeoCodingClient, GeoCodingClient>();
        services.AddScoped<IWeatherClient, WeatherClient>();
        services.AddScoped<IOpenMeteoMappingService, OpenMeteoMappingService>();
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
    }
}