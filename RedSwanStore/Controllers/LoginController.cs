using Microsoft.AspNetCore.Mvc;

namespace RedSwanStore.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        [Route("")]
        public IActionResult Login()
        {
            return View();
        }
    }
}