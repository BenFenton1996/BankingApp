using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaProject.Models;
using SocialMediaProject.Entities.Models;
using SocialMediaProject.Entities.Services;

namespace SocialMediaProject
{
    public class LoginController : Controller
    {
        private readonly IUsersService UsersService;
        public LoginController(IUsersService UsersService)
        {
            this.UsersService = UsersService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginStageOne()
        {
            //If user is already logged in then redirect them to the Timeline page
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Timeline", new { Area = "Home" });
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginStageOne(LoginStageOneViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User currentUser = UsersService.CheckUserDetails(viewModel.Email, viewModel.Password);
                if (currentUser != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, currentUser.Username),
                        new Claim("UserID", currentUser.UserID.ToString()),
                        new Claim(ClaimTypes.Role, currentUser.Role)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        IssuedUtc = DateTime.Now
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Timeline", new { Area = "Home" });
                }
                else
                {
                    ModelState.AddModelError("Incorrect", "Email or Password are incorrect");
                }
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public RedirectToActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LoginStageOne");
        }
    }
}