using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeatherForecastExample.Blazor.Client;
using MatBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var hostEnvironmentBaseAddress = builder.HostEnvironment.BaseAddress;
var baseAddress = new Uri("https://localhost:5001/");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = baseAddress });
builder.Services.AddWeatherForecastClient(baseAddress);
builder.Services.AddMatBlazor();

await builder.Build().RunAsync();