using authentication_security.Core.Application.Common;
using authentication_security.Core.Application.Interfaces;
using authentication_security.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using system_authentication_security.Middleware;
using authentication_security.Core.Application.Helper;

namespace system_authentication_security.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly UserViewModel _userViewModel;
    private readonly IHttpContextAccessor _contextAccessor;
    public UserController
        (IUserService userService, 
        IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _contextAccessor = contextAccessor;
        _userViewModel = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
    }

    public async Task<IActionResult> Index()
    {
        if(_userViewModel != null)
        {
            return View(await _userService.GetUser(_userViewModel.Id));
        }
        else
        {
            return RedirectToAction("Index", "Login");
        }
        
    }

    [ServiceFilter(typeof(LoginAuthorize))]
    public IActionResult Register()
    {
        return View(new SaveUserViewModel());
    }

    [HttpPost]
    [ServiceFilter(typeof(LoginAuthorize))]
    public async Task<IActionResult> Register(SaveUserViewModel saveUserVm)
    {
        if (!ModelState.IsValid)
        {
            return View(saveUserVm);
        }

        Result result = await _userService.AddUser(saveUserVm);
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error;
            return View(saveUserVm);
        }

        return RedirectToAction("Index", "Login");
    }  
}