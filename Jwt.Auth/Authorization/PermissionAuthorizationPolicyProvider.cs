using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Jwt.Auth.Authorization;

public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }
    
    // https://youtu.be/SZtZuvcMBA0?list=PLYpjLpq5ZDGtJOHUbv7KHuxtYLk1nJPw5&t=787
    
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
        {
            return policy;
        }

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();
    }
}