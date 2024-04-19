namespace Jwt.Auth.Authorization;

public class ApiKeyAuthenticationEndpointFilter(IConfiguration configuration): IEndpointFilter
{
    private const string ApiKeyHeaderName = "X-Api-Key";
    
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        string? apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];
        
        // The API key could also be specified in a cookie or query string parameter.
        var x= context.HttpContext.Request.Cookies[ApiKeyHeaderName];
        var x2= context.HttpContext.Request.Query["apiKey"];
        
        if (!IsApiKeyValid(apiKey))
        {
            return Results.Unauthorized();
        }
        
        return await next(context);
    }
    
    private bool IsApiKeyValid(string? apiKey)
    {
        if(string.IsNullOrEmpty(apiKey))
        {
            return false;
        }
        
        // This approach only allows for one API key, in reality each customer would have their own API key. In this
        // case it may be preferable to store the keys in a database.
        var actualApiKey = configuration.GetValue<string>("ApiKey");
        
        return apiKey == actualApiKey;
    }
}