using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

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
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
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

            result.RedirectLink = $"{Url.Action("Index", "Home")}";
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