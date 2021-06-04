using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace RedSwanStore.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("sign-up")]
    public class SignUpController : Controller
    {
        private readonly IUserRepo usersTable;

        public SignUpController(IUserRepo userR)
        {
            usersTable = userR;
        }
        
        [Microsoft.AspNetCore.Mvc.Route("")]
        public IActionResult SignUp()
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


        [Microsoft.AspNetCore.Mvc.Route("data-check")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult RegisterUser(string name, string surname, string login, string email, string password, bool getNewsOnEmail)
        {
            if (usersTable.GetUserByEmail(email) != null)
                return Content("email");
            
            var result = usersTable.AddUser(name, surname, login, email, password, getNewsOnEmail);

            if (!result.StartsWith("{\"success\":true"))
                return Content(result);
            
            return Content($"{Url.Action("Login", "Login")}");
        }
    }
}