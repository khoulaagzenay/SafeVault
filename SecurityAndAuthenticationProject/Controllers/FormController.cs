using Microsoft.AspNetCore.Mvc;
using SecurityAndAuthenticationProject.Helper;
using SecurityAndAuthenticationProject.Models;
using SecurityAndAuthenticationProject.Services;

namespace SecurityAndAuthenticationProject.Controllers
{
    public class FormController : Controller
    {
            private readonly AuthService _authService;

            public FormController(AuthService authService)
            {
                _authService = authService;
            }


            [HttpGet("/form")]
            public IActionResult Index()
            {
                var users = _authService.GetAllUsers();
                ViewData["Users"] = users;
                return View(new UserInput());
            }

            [HttpPost("/form")]
            public IActionResult Submit(UserInput input, string action)
            {
                if (!InputValidator.IsValidInput(input.Username, "@#$") ||
                    !InputValidator.IsValidInput(input.Email, "@._"))
                {
                    ModelState.AddModelError("", "Invalid input: disallowed characters or potential XSS detected.");
                    ViewData["Users"] = _authService.GetAllUsers();
                    return View("Index", input);
                }

                if (action == "register")
                {
                    _authService.StoreUser(input, role: "User");
                    ModelState.Clear();
                    ViewData["Users"] = _authService.GetAllUsers();
                    return View("Index", new UserInput());
                }
                else if (action == "login")
                {
                    if (_authService.AuthenticateUser(input.Username, input.Password))
                    {
                        HttpContext.Session.SetString("Username", input.Username);
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", "Invalid credentials");
                    ViewData["Users"] = _authService.GetAllUsers();
                    return View("Index", input);
                }

                ModelState.AddModelError("", "Unknown action.");
                ViewData["Users"] = _authService.GetAllUsers();
                return View("Index", input);
            }
    }
}

