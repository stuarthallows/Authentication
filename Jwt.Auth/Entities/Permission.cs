using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jwt.Auth.Entities;

public class Permission
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;
}

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        var permissions = Enum.GetValues<Authorization.Permission>()
            .Select(p => new Permission { Id = (int)p, Name = p.ToString() });
        
        builder.HasData(permissions);
    }
}
