using lr9.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Newtonsoft.Json;

namespace lr9.Controllers
{
    public class HomeController : Controller
    {
        private readonly OpenWeatherModel _openWeatherModel;
        private readonly ILogger<HomeController> _logger;
        List<ProductModel> products = new List<ProductModel>
        {
            new ProductModel(),
            new ProductModel(),
            new ProductModel("Sakuranbo", 200) 
        };
        public HomeController(ILogger<HomeController> logger, OpenWeatherModel openWeatherModel)
        {
            _logger = logger;
            _openWeatherModel = openWeatherModel;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return View();
            }
            
            string weather = await _openWeatherModel.GetWeather(city);
            WeatherDataModel data = JsonConvert.DeserializeObject<WeatherDataModel>(weather);
            return View(data);
        }
        public IActionResult Products()
        {  return View(products); }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}