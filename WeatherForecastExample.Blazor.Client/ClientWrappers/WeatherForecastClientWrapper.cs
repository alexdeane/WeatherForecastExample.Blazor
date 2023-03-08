using OneOf;
using WeatherForecastExample.Blazor.Client.Models;
using WeatherForecastExample.Grpc;
using WeatherForecastData = WeatherForecastExample.Blazor.Client.Models.WeatherForecastData;

namespace WeatherForecastExample.Blazor.Client.ClientWrappers;

public interface IWeatherForecastClientWrapper
{
    Task<OneOf<WeatherForecastDto, ErrorDto>>
        GetForecasts(string search, CancellationToken cancellationToken = default);
}

public class WeatherForecastClientWrapper : IWeatherForecastClientWrapper
{
    private readonly WeatherForecast.WeatherForecastClient _weatherForecastClient;

    public WeatherForecastClientWrapper(WeatherForecast.WeatherForecastClient weatherForecastClient)
    {
        _weatherForecastClient = weatherForecastClient;
    }

    public async Task<OneOf<WeatherForecastDto, ErrorDto>> GetForecasts(string search,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _weatherForecastClient.GetForecastsAsync(new WeatherForecastSearch
            {
                Search = search
            }, cancellationToken: cancellationToken);

            return response.ResultCase switch
            {
                WeatherForecastResponse.ResultOneofCase.Success => MapData(response.Success),
                WeatherForecastResponse.ResultOneofCase.Error => new ErrorDto(response.Error.UserSafeErrorMessage),
                _ => throw new ApplicationException("Unexpected result type")
            };
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return new ErrorDto("Failed to communicate with backend");
        }
    }

    private static WeatherForecastDto MapData(WeatherForecastSuccess result)
        => new()
        {
            Data = result.Data.Select(x => new WeatherForecastData
            {
                Date = DateTime.Parse(x.Date),
                Temperatures = x.Temperatures.ToArray()
            })
        };
}