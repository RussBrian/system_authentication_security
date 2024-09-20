using authentication_security.Core.Application.Common;
using authentication_security.Core.Application.ViewModels;

namespace authentication_security.Core.Application.Interfaces;

public interface ILoginService
{
    Task<Result<UserViewModel>> AuthenticateUser(LogInRequestViewModel requestVm);
    Task InvalidateTokenForUser(string userId);
}