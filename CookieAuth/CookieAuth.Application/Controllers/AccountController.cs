using CookieAuth.Application.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookieAuth.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {
            var dbUser = await _authService.ValidateUser(username, password);

            if (dbUser == null)
            {
                TempData["ErrorMessage"] = "Invalid username or password.";

                return View("login");
            }

            // Create Claims 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, dbUser.Name),
                new Claim("login", dbUser.Login)
            };

            // For test role authorize
            if (dbUser.Login == "vladimir1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            // Create Identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create Principal 
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Sign In
            await HttpContext.SignInAsync(claimsPrincipal);

            // Redirect
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }

        [HttpGet("denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
