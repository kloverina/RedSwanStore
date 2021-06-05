using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Controllers
{
    [Route("error")]
    public class ErrorPageController : Controller
    {
        private readonly IUserRepo usersTable;

        public ErrorPageController(IUserRepo userR)
        {
            usersTable = userR;
        }
        
        [Route("game-not-found")]
        [HttpGet]
        public IActionResult GameNotFound(string gameId)
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
                
                ViewData["userLogin"] = user.Login;
                ViewData["userUrl"] = user.UserUrl;
                ViewData["userPhoto"] = user.Photo;
                ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            }

            ViewBag.GameId = gameId;

            return View("GameNotFound");
        }
    }
}