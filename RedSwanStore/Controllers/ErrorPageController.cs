using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Utils;

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

                ViewBag.User = user;
                ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            }

            ViewBag.GameId = gameId;

            return View("GameNotFound");
        }

        
        [Route("access-denied")]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = usersTable.GetUserByEmail(User.Identity.Name!)!;

                ViewBag.User = user;
                ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            }

            return View("AccessDenied");
        }
    }
}