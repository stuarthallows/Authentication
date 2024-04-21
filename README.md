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
- `curl -i -H "Authorization: Bearer <token from dotnet user-jwts>" http://localhost:<port>/api/greetings/hello`


 

