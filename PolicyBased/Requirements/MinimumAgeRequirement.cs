using Microsoft.AspNetCore.Authorization;

namespace PolicyBased.Requirements;

// An authorization requirement is a collection of data parameters that a policy can use to evaluate the current user 
// principal. If an authorization policy contains multiple authorization requirements, all requirements must pass in
// order for the policy evaluation to succeed. A requirement doesn't need to have data or properties.

public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;
}
