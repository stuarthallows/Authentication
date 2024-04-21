using Microsoft.AspNetCore.Authorization;

namespace RequirementData.Authorization;

class MinimumAgeAuthorizeAttribute : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
{
    public MinimumAgeAuthorizeAttribute(int age) => Age = age;
    public int Age { get; }

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}