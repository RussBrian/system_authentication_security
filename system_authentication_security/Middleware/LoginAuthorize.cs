using Microsoft.AspNetCore.Mvc.Filters;
using system_authentication_security.Controllers;

namespace system_authentication_security.Middleware;

public class LoginAuthorize(ValidateUserSession validateUserSession) : IAsyncActionFilter
{
    private readonly ValidateUserSession _validateUserSession = validateUserSession;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (_validateUserSession.HasUser())
        {
            var controller = (UserController)context.Controller;

            context.Result = controller.RedirectToAction("Index", "User");
        }
        else
        {
            await next();
        }
    }
}

