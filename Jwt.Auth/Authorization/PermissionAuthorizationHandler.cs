using Jwt.Auth.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Jwt.Auth.Authorization;

public class PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        // Does the currently authenticated user have the required permission?
        string? userId = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        
        if(!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return;
        }
        
        // How to check whether the user has the required permission?
        // 1. Get the user's roles from the DB on each request, or
        // 2. Store the user's roles in the JWT token, and check the roles from the token, this does not allow updating the perms until the token expires.
        
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        
        IPermissionRepository permissionRepository = scope.ServiceProvider.GetRequiredService<IPermissionRepository>();
        
        HashSet<string> permissions = await permissionRepository.GetPermissionsAsync(parsedUserId);
        // TODO add caching here, so DB not hit on each request
        
        // Or, get the permissions from the JWT token
        var tokenPermissions = 
            context.User.Claims
                .Where(c => c.Type == CustomClaims.Permissions)
                .Select(c => c.Value)
                .ToHashSet();
        
        if(permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}