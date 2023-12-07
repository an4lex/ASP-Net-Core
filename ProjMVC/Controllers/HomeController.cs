using Microsoft.AspNetCore.Mvc;
using MVCProject_.Models;
using MVCProject_.ViewModels;

namespace MVCProject_.Controllers
{
    public class HomeController : Controller
    {
        public static List<User> users = new List<User>();
            
        public IActionResult Index()
        {
            return View("Registration");
        }
        public IActionResult UserInfo(User user)
        {
            if (ModelState.IsValid)
            {
                users.Add(user);
                Response.Cookies.Append("authorizationId", user.Id.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddMinutes(60),
                    IsEssential = true
                });
                return View("UserInfo", user);
            }
            return View("BelowAge");

        }

        public IActionResult OrderData(Int32 amount)
        {
            if(amount > 0)
            {
                int.TryParse(Request.Cookies["authorizationId"], out int id);
                User? user = users.FirstOrDefault(u => u.Id == id);
                for (int i = 0; i < amount; i++)
                {
                    user.Products.Add(new Product());
                }
                if (user.Age <= 16)
                {
                    return View("BelowAge");
                }
                else return View("OrderData", user);
            }
            return View("BelowProducts");
        }
        [HttpPost]
        public IActionResult ShowOrderResultData(User user)
        {
            int.TryParse(Request.Cookies["authorizationId"], out int id);
            User? actUser = users.FirstOrDefault(u => u.Id == id);
            if(actUser != null)
            {
                actUser.Products = user.Products;
            }
            return View(actUser);
        }

    }
}
