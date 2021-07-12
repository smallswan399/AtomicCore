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
            return NoContent();
        }
        
        /// <summary>
        /// Get请求
        /// </summary>
        /// <returns></returns>
        public IActionResult Get()
        {
            return null;
        }
    }
}
