using WeatherForecastExample.Blazor.Server.Web;
using WeatherForecastExample.Blazor.Server.Web.Servers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationDependencies();

builder.Services.AddGrpc();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

var app = builder.Build();

app.UseRouting();

app.UseGrpcWeb(new GrpcWebOptions
{
    DefaultEnabled = true
});

app.UseCors();

app.MapGrpcService<WeatherForecastServer>()
    .RequireCors("AllowAll");

app.Run();