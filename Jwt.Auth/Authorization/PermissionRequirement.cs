using Microsoft.AspNetCore.Authorization;

namespace Jwt.Auth.Authorization;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}