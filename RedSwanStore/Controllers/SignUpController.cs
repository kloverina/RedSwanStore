using Microsoft.AspNetCore.Mvc;

namespace RedSwanStore.Controllers
{
    [Route("sign-up")]
    public class SignUpController : Controller
    {
        [Route("")]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}