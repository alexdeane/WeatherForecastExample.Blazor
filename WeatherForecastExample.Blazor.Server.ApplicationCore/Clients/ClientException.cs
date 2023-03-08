namespace WeatherForecastExample.Blazor.Server.ApplicationCore.Clients;

public class ClientException : ApplicationException
{
    public ClientException(string userSafeMessage) : base(userSafeMessage)
    {
    }

    public ClientException(string userSafeMessage, Exception inner) : base(userSafeMessage, inner)
    {
    }
}