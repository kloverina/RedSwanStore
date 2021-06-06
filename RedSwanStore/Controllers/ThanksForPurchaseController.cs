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
        private readonly ICartRepo cartTable;

        public ThanksForPurchaseController(IUserRepo userR, ICartRepo cartR)
        {
            usersTable = userR;
            cartTable = cartR;
        }
        
        [Route("")]
        [HttpGet]
        public IActionResult ThanksForPurchase()
        {
            User user = usersTable.GetUserByEmail(User.Identity.Name!)!;
            
            cartTable.DeleteItem(user.Email);
                
            ViewData["userLogin"] = user.Login;
            ViewData["userUrl"] = user.UserUrl;
            ViewData["userPhoto"] = user.Photo;
            ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";

            return View();
        }
    }
}