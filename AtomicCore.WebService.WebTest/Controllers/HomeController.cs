using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AtomicCore.WebService.WebTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var appsetting = ConfigurationJsonManager.AppSettings;
            var connection = ConfigurationJsonManager.ConnectionStrings;

            string testKey = appsetting["TestKey"];

            string dbs = string.Join("||||", connection.Select(s => s.Value.ConnectionString));

            return Ok($"{testKey}-----{dbs}-----{DateTime.Now}");
        }
    }
}
