using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using late.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace late.Controllers
{
    public class AdminController : BaseController
    {
        private readonly AdminViewModel _admin;

        public AdminController(IOptions<AdminViewModel> adminOptions)
        {
            _admin = adminOptions.Value;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(AdminViewModel viewModel, string returnUrl)
        {
            if (viewModel.UserName == _admin.UserName && viewModel.Password == _admin.Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, viewModel.UserName),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                if (string.IsNullOrWhiteSpace(returnUrl))
                {

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                return Redirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "用户名或者密码错误!");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Jwt");
            return RedirectToAction("Index", "Home");
        }
    }
}