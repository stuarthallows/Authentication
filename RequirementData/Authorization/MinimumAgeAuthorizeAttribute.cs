using Microsoft.AspNetCore.Authorization;

namespace RequirementData.Authorization;

class MinimumAgeAuthorizeAttribute(int age) : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
{
    public int Age { get; } = age;

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}