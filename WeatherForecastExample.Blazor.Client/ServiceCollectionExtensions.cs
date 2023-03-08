using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using WeatherForecastExample.Blazor.Client.ClientWrappers;
using WeatherForecastExample.Grpc;

namespace WeatherForecastExample.Blazor.Client;

public static class ServiceCollectionExtensions
{
    public static void AddWeatherForecastClient(this IServiceCollection services, Uri baseUri)
    {
        services.AddScoped<IWeatherForecastClientWrapper, WeatherForecastClientWrapper>();
        services.AddSingleton(sp => 
        { 
            var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler())); 
            var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpClient = httpClient }); 
            return new WeatherForecast.WeatherForecastClient(channel); 
        });
    }
}