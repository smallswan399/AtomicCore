using Microsoft.AspNetCore.Mvc;

namespace AtomicCore.IOStorage.StoragePort.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("<a href='/BizIOStorageProvider.svc' target='_blank'>查看服务地址</a>");
        }
    }
}
