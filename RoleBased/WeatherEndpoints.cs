using Microsoft.AspNetCore.Http.HttpResults;

namespace RoleBased;

public static class WeatherWebApplicationExtensions
{
    public static IEndpointRouteBuilder MapWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        var builder = app.MapGroup("api/weather").RequireAuthorization().WithOpenApi();
        
        builder.MapGet("/forecast", WeatherEndpoints.GetWeatherForecast);
        
        return app;
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public static class WeatherEndpoints
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public static Ok<WeatherForecast[]> GetWeatherForecast() 
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
            .ToArray();
        
        return TypedResults.Ok(forecast);
    }
}
