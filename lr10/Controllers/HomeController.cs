using lr10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lr10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public bool isValidDate(ConsultationModel consultationModel)
        {
            if(consultationModel.Date <= DateTime.Now)
            {
                ModelState.AddModelError("Date", "The date must be in the future");
                return false;
            }
            if(consultationModel.Date.DayOfWeek == DayOfWeek.Saturday 
                || consultationModel.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                ModelState.AddModelError("Date", "Consultations are not available on weekends");
                return false;
            }
            if(consultationModel.SelectedLanguage == "Basis" && consultationModel.Date.DayOfWeek == DayOfWeek.Monday)
            {
                ModelState.AddModelError("Date", "Consultations 'Basis' are not available on Mondays");
                return false;
            }
            return true;
        }
        
        public IActionResult Index(ConsultationModel consultationModel)
        {
            if(isValidDate(consultationModel))
            {
                return RedirectToAction("SuccessForm", consultationModel);
            }
            return View();
        }
        public IActionResult SuccessForm(ConsultationModel consultationModel)
        {
            return View(consultationModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}