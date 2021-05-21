using Microsoft.AspNetCore.Mvc;

namespace RedSwanStore.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        [Route("")]
        public ViewResult Login()
        {
            return View();
        }
    }
}