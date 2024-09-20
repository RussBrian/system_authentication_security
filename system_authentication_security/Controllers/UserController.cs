using authentication_security.Core.Application.Common;
using authentication_security.Core.Application.Interfaces;
using authentication_security.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using system_authentication_security.Middleware;

namespace system_authentication_security.Controllers;

public class UserController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

    public IActionResult Index()
    {
        return View();
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