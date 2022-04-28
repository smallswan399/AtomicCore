using Microsoft.AspNetCore.Mvc;

namespace AtomicCore.WebService.WebTest.Controllers
{
    public class CryptoJSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
