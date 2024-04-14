using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

namespace NewAuth;

public static class DummyWebApplicationExtensions
{
    public static WebApplication MapDummyEndpoints(this WebApplication app)
    {
        var builder = app.MapGroup("").RequireAuthorization().WithOpenApi();
        
        builder.MapGet("", DummyEndpoints.SayHello);
        
        return app;
    }
}

public static class DummyEndpoints
{
    public static Ok<string> SayHello(ClaimsPrincipal user) 
    {
        return TypedResults.Ok($"Hello, {user.Identity!.Name}");
    }
}
