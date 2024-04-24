using Microsoft.AspNetCore.Authorization;

namespace PolicyBased.AuthHandlers;

public class MyHandler2 : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        throw new NotImplementedException();
    }
}