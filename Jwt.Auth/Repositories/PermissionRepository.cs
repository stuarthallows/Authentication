using Microsoft.EntityFrameworkCore;

namespace Jwt.Auth.Repositories;

public interface IPermissionRepository
{
    Task<HashSet<string>> GetPermissionsAsync(Guid userId);
}

public class PermissionRepository(AppDbContext context) : IPermissionRepository
{
    public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
    {
        var roles = await context.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(r => r.Name)
            .ToHashSet();
    }
}
