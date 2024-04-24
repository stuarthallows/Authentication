using Microsoft.AspNetCore.Authorization;

namespace PolicyBased.AuthHandlers;

public class MyHandler1 : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        throw new NotImplementedException();
    }
}