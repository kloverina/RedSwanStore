using Microsoft.AspNetCore.Mvc;

namespace RedSwanStore.Controllers
{
    [Route("library")]
    public class LibraryController : Controller
    {
        [Route("")]
        public IActionResult Library()
        {
            return View();
        }
    }
}