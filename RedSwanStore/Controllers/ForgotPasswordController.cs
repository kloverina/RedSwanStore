using Microsoft.AspNetCore.Mvc;

namespace RedSwanStore.Controllers
{
    [Route("forgot-password")]
    public class ForgotPasswordController : Controller
    {
        [Route("")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}