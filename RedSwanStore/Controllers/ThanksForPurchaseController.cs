using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Models;
using RedSwanStore.Utils;

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
                
            ViewBag.User = user;
            ViewData["layout"] = "~/Views/Shared/_AuthorizedLayout.cshtml";

            return View();
        }
    }
}