using Microsoft.AspNetCore.Authorization;
using PolicyBased.AuthHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Handlers can be registered using any of the built-in service lifetimes.
builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, MyHandler1>();
builder.Services.AddSingleton<IAuthorizationHandler, MyHandler2>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

builder.Services
       .AddAuthorizationBuilder()
       .AddPolicy("Something", policy => policy.RequireClaim("Permission", "CanViewPage", "CanViewAnything"));
// Require a policy with [Authorize(Policy = "Something")], or RequireAuthorization("Something")

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();