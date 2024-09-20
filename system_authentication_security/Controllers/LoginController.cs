using authentication_security.Core.Application.Helper;
using authentication_security.Core.Application.Interfaces;
using authentication_security.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using system_authentication_security.Middleware;

namespace system_authentication_security.Controllers
{
    public class LoginController
        (ILoginService loginService
        ) 
        : Controller
    {
        private readonly ILoginService _loginService = loginService;

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LogInRequestViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LogInRequestViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Los campos no pueden estar vacios";
                return View(loginVm);
            }

            var result = await _loginService.AuthenticateUser(loginVm);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Error;
                return View(loginVm);
            }

            HttpContext.Session.Set<UserViewModel>("user", result.Value);

            return RedirectToRoute(new { controller = "User", action = "Index" });

        }

        [HttpPost]
        public async Task<IActionResult> Logout(string id)
        {
            await _loginService.InvalidateTokenForUser(id);
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Login");
        }
        
    }
}
