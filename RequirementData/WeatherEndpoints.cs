using Microsoft.AspNetCore.Http.HttpResults;
using RequirementData.Authorization;

namespace RequirementData;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public static class WeatherWebApplicationExtensions
{
    public static WebApplication MapWeatherEndpoints(this WebApplication app)
    {
        var builder = app.MapGroup("api/weather").RequireAuthorization().WithOpenApi();
        
        builder.MapGet("/forecast", WeatherEndpoints.GetWeatherForecast);
        
        return app;
    }
}

public static class WeatherEndpoints
{
    [MinimumAgeAuthorize(33)]
    public static Ok<WeatherForecast[]> GetWeatherForecast() 
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        
        return TypedResults.Ok(forecast);
    }
}