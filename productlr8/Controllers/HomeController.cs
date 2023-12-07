using Microsoft.AspNetCore.Mvc;
using productlr8.Models;
using System.Diagnostics;

namespace productlr8.Controllers
{
    public class HomeController : Controller
    {
        public static bool swap = false;
        public static List<Product> products = new List<Product>{
            new Product("Product 1", 500),
            new Product("Product 2", 600),
            new Product("Product 3", 700)
        };

        public IActionResult Index()
        {
            ViewBag.Swap = swap;
            return View(products);
        }
        public IActionResult Swap()
        {
            swap = !swap;
            return RedirectToAction("Index");
        }
        
    }
}