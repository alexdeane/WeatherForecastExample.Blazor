using System.Globalization;
using WeatherForecastExample.Blazor.Server.ApplicationCore.Clients.Contracts;
using WeatherForecastExample.Grpc;

namespace WeatherForecastExample.Blazor.Server.ApplicationCore.Services;

/// <summary>
/// This service exists to map the OpenMeteo Weather API
/// response into a more usable form for the frontend.
/// </summary>
public interface IOpenMeteoMappingService
{
    public WeatherForecastSuccess Map(OpenMeteoWeatherResponse openMeteoWeatherResponse,
        OpenMeteoLocationResult location);
}

/// <summary>
/// This logic is nice to have in its own service simply
/// because it makes unit testing easier.
/// </summary>
public class OpenMeteoMappingService : IOpenMeteoMappingService
{
    public WeatherForecastSuccess Map(
        OpenMeteoWeatherResponse openMeteoWeatherResponse,
        OpenMeteoLocationResult location)
    {
        var temperatures = openMeteoWeatherResponse.Hourly?.Temperature2M;
        var dates = openMeteoWeatherResponse.Hourly?.UtcTime;

        var success = new WeatherForecastSuccess
        {
            Name = location.Name,
            Country = location.Country,
            TimeZone = location.TimeZone,
            TimeUnit = openMeteoWeatherResponse.HourlyUnits?.Time,
            TemperatureUnit = openMeteoWeatherResponse.HourlyUnits?.Temperature2M,
        };

        AddData(success, dates, temperatures);

        return success;
    }

    private static void AddData(
        WeatherForecastSuccess success,
        IEnumerable<DateTime> dates,
        IEnumerable<float> temperatures)
    {
        var data = temperatures
            .Zip(dates, (temp, t) =>
                new
                {
                    Temperature = temp,
                    Date = t.Date,
                    Time = t.Date.TimeOfDay
                }
            )
            .GroupBy(x => x.Date)
            .ToDictionary(x => x.Key, x => x.OrderBy(y => y.Time).ToArray())
            .Select(x =>
            {
                var data = new WeatherForecastData
                {
                    Date = x.Key.ToString(CultureInfo.InvariantCulture)
                };

                data.Temperatures.AddRange(x.Value.Select(y => y.Temperature));

                return data;
            });

        success.Data.AddRange(data);
    }
}