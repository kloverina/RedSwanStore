using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Utils;

namespace RedSwanStore.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly IUserRepo usersTable;

        public LoginController(IUserRepo userR)
        {
            usersTable = userR;
        }
        
        [Route("")]
        [HttpGet]
        public ViewResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            
            if (User.Identity.IsAuthenticated)
            {
                User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
                
                ViewBag.User = user;
                ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            }
            
            return View();
        }


        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        
        [HttpPost]
        public IActionResult Login(string email, string password, string returnUrl)
        {
            var result = new LoginResult();
            User? user = usersTable.GetUserByEmail(email);

            if (user is null)
                return Json(result);

            result.IsCorrectEmail = true;
            
            if (password != user.Password)
                return Json(result);

            result.IsCorrectPassword = true;
            
            Authenticate(email);
            result.RedirectLink = string.IsNullOrEmpty(returnUrl) ? $"library/user?{user.UserUrl}": $"{returnUrl}";
            return Json(result);
        }


        private void Authenticate(string email)
        {
            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };
            
            var identifier = new ClaimsIdentity(
                claims, 
                "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identifier));
        }
    }


    public class LoginResult
    {
        public bool IsCorrectEmail { get; set; } = false;
        public bool IsCorrectPassword { get; set; } = false;
        public string RedirectLink { get; set; } = "";
    }
}