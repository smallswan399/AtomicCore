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


            return Ok(DateTime.Now.ToString());
        }
    }
}
