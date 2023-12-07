using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FileDownload.Controllers
{
    public class FileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public FileResult GenerateFile(string firstName, string lastName, string fileName)
        {
            string content = $"First Name: {firstName}{Environment.NewLine}Last name: {lastName}";

            var byteArray = Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(byteArray);

            return File(stream, "text/plain", $"{fileName}.txt");
        }
    }
}
