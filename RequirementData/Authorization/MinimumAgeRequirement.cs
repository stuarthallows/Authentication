using Microsoft.AspNetCore.Authorization;

namespace RequirementData.Authorization;

class MinimumAgeRequirement : IAuthorizationRequirement
{
    public int Age { get; private set; }

    public MinimumAgeRequirement(int age) { Age = age; }
}
