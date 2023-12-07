using lr11.Filters;
using lr11.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace lr11.Controllers
{

    [UnqiueLogin]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Log]
        public IActionResult Test()
        {
            return View();
        }
        [Log]
        public IActionResult Index()
        {
            return View();
        }
        [Log]
        
        public IActionResult Login(string username) {
            if (!string.IsNullOrEmpty(username))
            {
                HttpContext.Response.Cookies.Append("username", username);
                return RedirectToAction("Index");
            }
            return View();
        }
        [Log]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Log]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}