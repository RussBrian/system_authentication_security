using authentication_security.Core.Application.ViewModels;
using authentication_security.Core.Application.Common;

namespace authentication_security.Core.Application.Interfaces;
public interface IUserService
{
    Task<Result> AddUser(SaveUserViewModel userViewModel);
    Task<UserViewModel> GetUser(string userId);
}

