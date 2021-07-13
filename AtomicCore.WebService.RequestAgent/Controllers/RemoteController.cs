using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtomicCore.WebService.RequestAgent.Controllers
{
    /// <summary>
    /// 远程请求控制器
    /// </summary>
    public class RemoteController : Controller
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Content(DateTime.Now.ToString());
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <returns></returns>
        public IActionResult Get(string url)
        {
            if (string.IsNullOrEmpty(url))
                return Content("url null");

            string get_url = System.Web.HttpUtility.UrlDecode(url);

            Dictionary<string, string> get_heads = this.Request.Headers.ToDictionary(k => k.Key, v => v.Value.ToString());

            string res;
            try
            {
                res = HttpProtocol.HttpGet(get_url, string.Empty, get_heads, null);
            }
            catch (Exception ex)
            {
                return Content(null == ex.InnerException ? ex.Message : ex.InnerException.Message);
            }

            return Content(res);
        }
    }
}
