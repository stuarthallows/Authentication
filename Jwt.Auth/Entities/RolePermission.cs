using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jwt.Auth.Entities;

public class RolePermission
{
    public int RoleId { get; init; }
    public int PermissionId { get; init; }
}

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");

        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        
        // builder.HasOne(rp => rp.Role)
        //     .WithMany(r => r.Permissions)
        //     .HasForeignKey(rp => rp.RoleId);
        // builder.HasOne(rp => rp.Permission)
        //     .WithMany()
        //     .HasForeignKey(rp => rp.PermissionId);

        var data = new[]
        {
            new RolePermission { RoleId = 1, PermissionId = (int)Authorization.Permission.ReadUser },
            new RolePermission { RoleId = 2, PermissionId = (int)Authorization.Permission.UpdateUser }
            // Create(Role.Registered, Authorization.Permission.ReadUser),
            // Create(Role.Registered, Authorization.Permission.UpdateUser)
        };
        builder.HasData(data);
    }

    // private static RolePermission Create(Role role, Jwt.Auth.Authorization.Permission permission)
    // {
    //     return new RolePermission
    //     {
    //         RoleId = role.Value,
    //         PermissionId =  (int)permission
    //     };
    // }
}