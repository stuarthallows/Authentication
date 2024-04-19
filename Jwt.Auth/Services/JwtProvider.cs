using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Jwt.Auth.Authorization;
using Jwt.Auth.Entities;
using Jwt.Auth.Options;
using Jwt.Auth.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Auth.Services;

public interface IJwtProvider
{
    Task<string> GenerateAsync(User user);
}

internal sealed class JwtProvider(
    IPermissionRepository permissionRepository, 
    IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public async Task<string> GenerateAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };
        
        // Optionally include permissions in the JWT. The token is sent to the server with each request, so can have a
        // performance impact if the token size is large. Also, if the permissions change, the token must be re-issued
        // to add permissions, or revoked to remove permissions.
        HashSet<string> permissions = await permissionRepository.GetPermissionsAsync(user.Id);
        foreach (var permission in permissions)
        {
            claims.Add(new Claim(CustomClaims.Permissions, permission));
        }
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
