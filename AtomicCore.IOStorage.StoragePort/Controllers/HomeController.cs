using Microsoft.AspNetCore.Mvc;

namespace AtomicCore.IOStorage.StoragePort.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
