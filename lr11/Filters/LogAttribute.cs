using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace lr11.Filters
{
    public class LogAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string path = "file.txt";
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            string content = $"Method name: " +
                $"{descriptor.ActionName} Date:" +
                $" {DateTime.Now.ToString()}";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLineAsync(content);
            }
        }
    }
}
