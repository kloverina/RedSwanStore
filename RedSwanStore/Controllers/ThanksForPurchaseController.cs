using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;

namespace RedSwanStore.Controllers
{
    [Route("thanks-for-purchase")]
    [Authorize]
    public class ThanksForPurchaseController : Controller
    {
        private readonly IUserRepo usersTable;

        public ThanksForPurchaseController(IUserRepo userR)
        {
            usersTable = userR;
        }
        
        [Route("")]
        [HttpGet]
        public IActionResult ThanksForPurchase()
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
                
            ViewData["userLogin"] = user.Login;
            ViewData["userUrl"] = user.UserUrl;
            ViewData["userPhoto"] = user.Photo;
            ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";

            return View();
        }
    }
}