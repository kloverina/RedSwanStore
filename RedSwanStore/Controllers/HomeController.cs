using Microsoft.AspNetCore.Mvc;

namespace RedSwanStore.Controllers
{
    [Route("")]
    [Route("store")]
    [Route("home")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}