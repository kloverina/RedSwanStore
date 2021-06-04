using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Controllers
{
    [Route("profile")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepo usersTable;

        public ProfileController(IUserRepo userR)
        {
            usersTable = userR;
        }
        
        [Route("user")]
        [HttpGet]
        public IActionResult Profile()
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
                
            ViewData["userLogin"] = user.Login;
            ViewData["userUrl"] = user.UserUrl;
            ViewData["userPhoto"] = user.Photo;
            ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            
            return View(user);
        }

        
        [HttpPost]
        public IActionResult SaveChanges(string login, string url, string name, string surname, string oldPassword, string newPassword)
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            UpdateResult result = new UpdateResult();

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