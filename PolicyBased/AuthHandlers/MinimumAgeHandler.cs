using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PolicyBased.Requirements;

namespace PolicyBased.AuthHandlers;

// An authorization handler is responsible for the evaluation of a requirement's properties. The authorization handler
// evaluates the requirements against a provided AuthorizationHandlerContext to determine if access is allowed.

// A requirement can have multiple handlers. A handler may inherit AuthorizationHandler<TRequirement>, where
// TRequirement is the requirement to be handled. Alternatively, a handler may implement IAuthorizationHandler directly
// to handle more than one type of requirement.

// The following example shows a one-to-one relationship in which a minimum age handler handles a single requirement.

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var dateOfBirthClaim = context.User.FindFirst(
            c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == "http://contoso.com");

        if (dateOfBirthClaim is null)
        {
            return Task.CompletedTask;
        }

        var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim.Value);
        var calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
        {
            calculatedAge--;
        }

        if (calculatedAge >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
