using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Utils;

namespace RedSwanStore.Controllers
{
    [Route("profile")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepo usersTable;
        private readonly IWebHostEnvironment appEnv;

        public ProfileController(IUserRepo userR, IWebHostEnvironment appEnv)
        {
            usersTable = userR;
            this.appEnv = appEnv;
        }
        
        [Route("user")]
        [HttpGet]
        public IActionResult Profile()
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
                
            ViewData["userLogin"] = user.Login;
            ViewData["userUrl"] = user.UserUrl;
            ViewData["userPhoto"] = user.Photo;
            ViewData["userBalance"] = user.Balance.ConvertToPrice();
            ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            
            return View(user);
        }

        
        [HttpPost]
        public IActionResult SaveChanges(IFormFile? photo, string login, string url, string name, string surname, string oldPassword, string newPassword)
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            UpdateResult result = new UpdateResult();

            if (photo != null)
            {
                string path = "/img/users-photos/" + photo.FileName;

                using (var fs = new FileStream(appEnv.WebRootPath + path, FileMode.Create))
                {
                    photo.CopyTo(fs);
                }
                
                usersTable.UpdateUserPhoto(user, ".." + path);
            }
            
            result.IsCorrectLogin = UpdateLogin(user, login);
            result.IsCorrectUrl = UpdateUrl(user, url);
            result.IsCorrectName = UpdateName(user, name);
            result.IsCorrectSurname = UpdateSurname(user, surname);

            if (!string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword))
            {
                result.IsCorrectOldPassword = user.Password == oldPassword;
                if (!result.IsCorrectOldPassword)
                    return Json(result);

                result.IsCorrectNewPassword = usersTable.UpdateUserPassword(user, newPassword);
            }

            return Json(result);
        }


        private bool UpdateLogin(User user, string login)
        {
            if (login == user.Login)
                return true;

            return usersTable.UpdateUserLogin(user, login);
        }

        private bool UpdateUrl(User user, string url)
        {
            if (url == user.UserUrl)
                return true;

            return usersTable.UpdateUserUrl(user, url);
        }

        private bool UpdateName(User user, string name)
        {
            if (name == user.Name)
                return true;

            return usersTable.UpdateUserName(user, name);
        }

        private bool UpdateSurname(User user, string surname)
        {
            if (surname == user.Surname)
                return true;

            return usersTable.UpdateUserSurname(user, surname);
        }
        
    }

    public class UpdateResult
    {
        public bool IsCorrectLogin { get; set; } = true;
        public bool IsCorrectUrl { get; set; } = true;
        public bool IsCorrectName { get; set; } = true;
        public bool IsCorrectSurname { get; set; } = true;
        public bool IsCorrectOldPassword { get; set; } = true;
        public bool IsCorrectNewPassword { get; set; } = true;
    }
}