using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtomicCore.WebService.WebTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var app = ConfigurationJsonManager.AppSettings;

            return Ok(DateTime.Now.ToString());
        }
    }
}
