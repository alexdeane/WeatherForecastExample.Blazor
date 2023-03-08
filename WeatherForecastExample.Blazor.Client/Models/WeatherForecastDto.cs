// ReSharper disable ClassNeverInstantiated.Global
namespace WeatherForecastExample.Blazor.Client.Models;

public record WeatherForecastDto
{
    public string? Name { get; init; }
    public string? Country { get; init; }
    public string? TimeZone { get; init; }
    public IEnumerable<WeatherForecastData>? Data { get; init; }
    public string? TimeUnit { get; init; }
    public string? TemperatureUnit { get; init; }
}

public class WeatherForecastData
{
    public DateTime? Date { get; init; }
    public float[]? Temperatures { get; init; }
}

public record ErrorDto(string UserSafeErrorMessage);