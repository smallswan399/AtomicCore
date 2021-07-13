using Microsoft.AspNetCore.Mvc;

namespace AtomicCore.WebService.RequestAgent.Controllers
{
    /// <summary>
    /// 起始控制器
    /// https://www.coder.work/article/446141
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 默认服务
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //string encodeUrl = System.Web.HttpUtility.UrlEncode("https://api-goerli.etherscan.io/api?module=account&action=balance&address=0x29aAe16abfDC4C6F119E86D09ab8603D491c5d5F&tag=latest&apikey=JD5TN6TN7KVGW21N7Q9ETH7FSIUTD4W4S2");

            return Content("welcome to io request agent port...");
        }
    }
}
