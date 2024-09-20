using authentication_security.Core.Application.Helper;
using authentication_security.Core.Application.ViewModels;
using Microsoft.AspNetCore.Http;

namespace system_authentication_security.Middleware;

public class ValidateUserSession(IHttpContextAccessor httpContextAccessor)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public bool HasUser()
    {
        UserViewModel authenticationResponse = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

        if (authenticationResponse == null)
        {
            return false;
        }
        return true;
    }
}


