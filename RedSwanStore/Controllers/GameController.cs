using Microsoft.AspNetCore.Mvc;

namespace RedSwanStore.Controllers
{
    [Route("game")]
    public class GameController : Controller
    {
        
        public IActionResult Game()
        {
            return View();
        }
    }
}