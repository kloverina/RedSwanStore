using Microsoft.AspNetCore.Mvc;

namespace RedSwanStore.Controllers
{
    [Route("profile")]
    public class ProfileController : Controller
    {
        // GET
        public IActionResult Profile()
        {
            return View();
        }
    }
}