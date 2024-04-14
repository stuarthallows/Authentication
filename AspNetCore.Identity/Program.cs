using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NewAuth;
using NewAuth.Services;

// TODO message returned from HTTP but unauthorized from Swagger

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

// If passing useCookies: true on login, the default scheme will be set to IdentityConstants.ApplicationScheme
// builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.ApplicationScheme);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlite("DataSource=app.db"));

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.Password = new PasswordOptions
        {
            RequireDigit = false,
            RequireLowercase = false,
            RequireNonAlphanumeric = false,
            RequireUppercase = false,
            RequiredLength = 6
        };
        options.Lockout = new LockoutOptions
        {
            DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
            MaxFailedAccessAttempts = 5,
            AllowedForNewUsers = true
        };
        options.User = new UserOptions
        {
            AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",
            RequireUniqueEmail = true
        };
        options.SignIn = new SignInOptions
        {
            RequireConfirmedEmail = true,
            RequireConfirmedPhoneNumber = false
        };
        options.ClaimsIdentity = new ClaimsIdentityOptions
        {
            RoleClaimType = ClaimTypes.Role,
            UserNameClaimType = ClaimTypes.Name,
            UserIdClaimType = ClaimTypes.NameIdentifier,
            SecurityStampClaimType = "AspNet.Identity.SecurityStamp"
        };
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddTransient<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>()
                .AddTransient<IEmailSender, ConsoleEmailSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<ApplicationUser>();

app.MapDummyEndpoints();

app.Run();

public class ApplicationUser : IdentityUser
{ }

internal class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options);
