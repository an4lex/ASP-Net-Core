using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace lr11.Filters
{
    public class UnqiueLoginAttribute : Attribute, IActionFilter
    {
        HashSet<string> usernames = new HashSet<string>();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string username = context.HttpContext.Request.Cookies["username"];
            if (username == null)
            {
                return;
            }
            string allUsers = "allUsers.txt";
            if(File.Exists(allUsers))
            {
                string[] existingUsers = File.ReadAllLines(allUsers);
                usernames = new HashSet<string>(existingUsers);
            }
            if(!usernames.Contains(username))
            {
                usernames.Add(username);
                File.WriteAllLines(allUsers, usernames);
            }
            string uniqueUsers = "uniqueUsers.txt";
            context.HttpContext.Response.Cookies.Append("username", username);
            usernames.Add(username);
            using (StreamWriter sw = new StreamWriter(uniqueUsers, false))
            {
                sw.WriteLine($"Count of unique users: {usernames.Count}, last login: {DateTime.Now.ToString()}");
            }
        }
    }
}
