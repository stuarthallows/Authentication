using Microsoft.AspNetCore.Authorization;

namespace RequirementData.Authorization;

class MinimumAgeRequirement(int age) : IAuthorizationRequirement
{
    public int Age { get; private set; } = age;
}
