using Microsoft.AspNetCore.Mvc;

namespace AtomicCore.IOStorage.StoragePort.Controllers
{
    /// <summary>
    /// 默认控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 默认Action
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Content("welcome to io storage port...");
        }
    }
}
