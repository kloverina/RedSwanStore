using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Utils;

namespace RedSwanStore.Controllers
{
    [Route("forgot-password")]
    public class ForgotPasswordController : Controller
    {
        private readonly IUserRepo usersTable;

        public ForgotPasswordController(IUserRepo userR)
        {
            usersTable = userR;
        }
        
        
        [Route("")]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
                
                
                ViewData["userLogin"] = user.Login;
                ViewData["userUrl"] = user.UserUrl;
                ViewData["userPhoto"] = user.Photo;
                ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            }
            
            return View();
        }


        [Route("")]
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var result = new RecoveryResult();
            User? user = usersTable.GetUserByEmail(email);

            
            if (user is null)
                return Json(result);

            
            result.IsCorrectEmail = true;
            result.RedirectLink = $"{Url.Action("Login", "Login")}";
            
            var emailService = new EmailService();
            result.ErrorMessage = emailService.SendPasswordRecoveryEmail(email, user.Name, user.Surname, GenerateNewPassword(user));
            
            return Json(result);
        }


        private string GenerateNewPassword(User user)
        {
            var passwordGenerationSeed = RandomNumberGenerator.GetInt32(int.MaxValue);
            var newPassword = HashKeccak.createHashOf(
                passwordGenerationSeed.ToString(),
                128
            ).Substring(0, 16);

            usersTable.UpdateUserPassword(user, newPassword);

            return newPassword;
        }
    }

    public class RecoveryResult
    {
        public bool IsCorrectEmail { get; set; } = false;
        public string ErrorMessage { get; set; } = "";
        public string RedirectLink { get; set; } = "";
    }
}