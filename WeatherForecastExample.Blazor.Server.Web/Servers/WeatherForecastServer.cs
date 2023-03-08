using Grpc.Core;
using WeatherForecastExample.Blazor.Server.ApplicationCore.Services;
using WeatherForecastExample.Grpc;

namespace WeatherForecastExample.Blazor.Server.Web.Servers;

public class WeatherForecastServer : WeatherForecast.WeatherForecastBase
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastServer(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }
    
    public override async Task<WeatherForecastResponse> GetForecasts(WeatherForecastSearch request,
        ServerCallContext context)
    {
        var result = await _weatherForecastService.GetForecasts(request.Search, context.CancellationToken);
        var response = new WeatherForecastResponse();

        result.Switch(success =>
        {
            response.Success = success;
        }, error =>
        {
            response.Error = error;
        });

        return response;
    }
}