using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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

            string res;
            try
            {
                res = HttpProtocol.HttpGet(get_url, string.Empty, null, null);
            }
            catch (Exception ex)
            {
                return Content(null == ex.InnerException ? ex.Message : ex.InnerException.Message);
            }

            return Content(res);
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public IActionResult Post(string url, string contentType = HttpProtocol.APPLICATIONJSON)
        {
            if (string.IsNullOrEmpty(url))
                return Content("url null");

            string post_url = System.Web.HttpUtility.UrlDecode(url);
            string post_date = null;
            if (this.Request.Form.Count > 0)
                post_date = string.Join("&", this.Request.Form.Select(s => string.Format("{0}={1}", s.Key, s.Value.ToString())));

            string res;
            try
            {
                res = HttpProtocol.HttpPost(post_url, post_date, contentType, null, null);
            }
            catch (Exception ex)
            {
                return Content(null == ex.InnerException ? ex.Message : ex.InnerException.Message);
            }

            return Content(res);
        }
    }
}
