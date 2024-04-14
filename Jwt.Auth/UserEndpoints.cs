using Jwt.Auth.Entities;
using Jwt.Auth.Repositories;
using Jwt.Auth.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Jwt.Auth;

public static class AuthWebApplicationExtensions
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        var builder = app.MapGroup("/api/users").WithOpenApi();

        builder.MapPost("register", UserEndpoints.RegisterUser).AllowAnonymous();
        builder.MapPost("login", UserEndpoints.LoginUser).AllowAnonymous();
        builder.MapGet("{id}", UserEndpoints.GetUserById).RequireAuthorization();
        
        return app;
    }
}


public static class UserEndpoints
{
    public record RegisterRequest(string Email, string FirstName, string LastName);
    public record LoginRequest(string Email);

    public static async Task<Results<Ok<User>, BadRequest<string>>> RegisterUser(RegisterRequest request, IUserRepository repository)
    {
        var isEmailUnique = await repository.IsEmailUniqueAsync(request.Email);

        if (!isEmailUnique)
        {
            return TypedResults.BadRequest("Email is already in use.");
        }

        var user = new User(Guid.NewGuid(), request.Email, request.FirstName, request.LastName);
        repository.Add(user);

        return TypedResults.Ok(user);
    }
    
    public static async Task<Results<Ok<string>, NotFound>> LoginUser(LoginRequest request, IJwtProvider jwtProvider, IUserRepository repository)
    {
        var user = await repository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return TypedResults.NotFound();
        }

        var token = jwtProvider.Generate(user);

        return TypedResults.Ok(token);
    }

    public static async Task<Results<Ok<User>, NotFound>> GetUserById(Guid id, IUserRepository repository)
    {
        var user = await repository.GetByIdAsync(id);
        
        if (user is null)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(user);
    }
}
