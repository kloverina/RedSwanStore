using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Controllers
{
    [Route("game")]
    public class GameController : Controller
    {
        private readonly IUserRepo usersTable;
        private readonly IGameRepo gamesTable;

        public GameController(IUserRepo userR, IGameRepo gameR)
        {
            usersTable = userR;
            gamesTable = gameR;
        }
        
        [Route("")]
        [HttpGet]
        public IActionResult Game(string gameId)
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
                
                ViewData["userLogin"] = user.Login;
                ViewData["userUrl"] = user.UserUrl;
                ViewData["userPhoto"] = user.Photo;
                ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";
            }

            Game? game = gamesTable.GetGameByUrl(gameId);

            if (game is null)
                RedirectToAction("Index", "Home");
            
            return View(game);
        }
    }
}