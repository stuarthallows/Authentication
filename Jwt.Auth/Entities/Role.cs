using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jwt.Auth.Entities;

public sealed class Role //: Enumeration<Role>
{
    // public static readonly Role Registered = new(1, nameof(Registered));

    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    // public Role(int id, string name) : base(id, name)
    // {
    // }
    //
    public ICollection<Permission> Permissions { get; init; }
    
    public ICollection<User> Users { get; set; }
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles"); // TODO see final code
        
        builder.HasKey(r => r.Id); // TODO is Id in the video
        builder.Property(r => r.Id).ValueGeneratedNever();
        
        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
        
        builder.HasMany(r => r.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();
        
        builder.HasMany(r => r.Users)
                .WithMany();

        var data = new[]
        {
            new Role { Id = 1, Name = "Registered" },
            new Role { Id = 2, Name = "Administrator" }
        };
        builder.HasData(data);
    }
}