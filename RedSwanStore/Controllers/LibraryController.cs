using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedSwanStore.Controllers
{
    [Route("library")]
    [Authorize]
    public class LibraryController : Controller
    {
        [Route("")]
        public IActionResult Library(string userUrl)
        {
            return View();
        }
    }
}