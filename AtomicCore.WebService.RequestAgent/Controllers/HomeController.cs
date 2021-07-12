using Microsoft.AspNetCore.Mvc;

namespace AtomicCore.WebService.RequestAgent.Controllers
{
    /// <summary>
    /// 起始控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 默认服务
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Content("welcome to io request agent port...");
        }
    }
}
