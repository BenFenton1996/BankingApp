using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Models;
using BankingApp.Entities.Models;
using BankingApp.Entities.Services;

namespace BankingApp
{
    public class LoginController : Controller
    {
        private readonly IUsersService UsersService;
        public LoginController(IUsersService UsersService)
        {
            this.UsersService = UsersService;
        }

        /// <summary>
        /// The View containing the LoginStageOne form with a modal for signing up with a new account
        /// </summary>
        /// <returns>The View containing the Login form</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginStageOne()
        {
            //If user is already logged in then redirect them to the Home page
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { Area = "Home" });
            }
            return View(new LoginStageOneViewModel());
        }

        /// <summary>
        /// Recieves User details, validates them and logs the user in with those details if successful
        /// </summary>
        /// <param name="viewModel">The viewModel containing the user details to validate</param>
        /// <returns>Redirects to the Home View in the Home Area if successful, otherwise returns the LoginStageOne View</returns>
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

                    return RedirectToAction("Index", "Home", new { Area = "Home" });
                }
                else
                {
                    ModelState.AddModelError("Incorrect", "Email or Password are incorrect");
                }
            }

            return View(new LoginStageOneViewModel());
        }

        /// <summary>
        /// Recieves User details for a new account and passes them to a service which 
        /// checks them to make sure they don't match the details of an existing account. 
        /// If they don't, create a new account.
        /// </summary>
        /// <param name="viewModel">The viewModel containing the User details for a new account</param>
        /// <returns>The LoginStageOne View</returns>
        [AllowAnonymous]
        [HttpPost]
        public ViewResult SignUp(SignUpViewModel viewModel)
        {
            bool accountCreated = false;
            if (ModelState.IsValid)
            {
                if (!UsersService.CreateNewAccount(viewModel.NewUsername, viewModel.NewEmail, viewModel.NewPassword))
                {
                    ModelState.AddModelError("AccountAlreadyExists", "Email or Username already in use.");
                }
                else
                {
                    ModelState.Clear();
                    accountCreated = true;
                }
            }

            return View("LoginStageOne", new LoginStageOneViewModel
            {
                AccountCreated = accountCreated
            });
        }

        /// <summary>
        /// Logs the user out
        /// </summary>
        /// <returns>The LoginStageOne View</returns>
        [Authorize]
        [HttpGet]
        public RedirectToActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LoginStageOne");
        }
    }
}