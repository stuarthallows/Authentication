# Authentication

## AspNetCore.Identity

## Jwt.Auth

 `dotnet ef migrations add <migration-name>`
 `dotnet ef database update`

## Requirement Data

[IAuthorizationRequirementData](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/iard?view=aspnetcore-8.0)

[Sample code](https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main/security/authorization/AuthRequirementsData)


Test the policy with [user-jwts](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-8.0&tabs=windows) and curl;
- `dotnet user-jwts create --claim http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth=1989-01-01`
- `curl -i -H "Authorization: Bearer <token from dotnet user-jwts>" https://localhost:7036/api/weather/forecast`

## Role Based

Roles are claims, but not all claims are roles.

## [Policy Based](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-8.0)

Under the covers role-based auth and claims-based auth both use a requirement, a requirement handler and a 
preconfigured policy. An auth policy consists of one or more requirements.

It's possible to bundle both a requirement and a handler into a single class implementing both IAuthorizationRequirement 
and `IAuthorizationHandler`. This bundling creates a tight coupling between the handler and requirement and is only 
recommended for simple requirements and handlers. Creating a class that implements both interfaces removes the need to 
register the handler in DI because of the built-in `PassThroughAuthorizationHandler` that allows requirements to handle 
themselves.

See the `AssertionRequirement` class for a good example where the AssertionRequirement is both a requirement and the 
handler in a fully self-contained class.

https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-8.0#what-should-a-handler-return

