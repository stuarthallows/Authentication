using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Jwt.Auth.Authorization;

// https://youtu.be/SZtZuvcMBA0?list=PLYpjLpq5ZDGtJOHUbv7KHuxtYLk1nJPw5&t=713

/// <summary>
/// Rather than explicitly create a new policy for each Permission requirements are created on demand.
/// </summary>
public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }
    
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        // Check if the policy is already defined.
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
        {
            return policy;
        }

        // If the policy is not defined, create a new one.
        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();
    }
}