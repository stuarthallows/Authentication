using Microsoft.AspNetCore.Authorization;

namespace Jwt.Auth.Authorization;

public class HasPermissionAttribute(Permission permission) : AuthorizeAttribute(policy: permission.ToString());