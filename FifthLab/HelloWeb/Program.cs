using HelloWeb;
using HelloWeb.Loggers;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "loggerErrors.txt"));
var app = builder.Build();
var logger = app.Logger;
// Token Middlweare || 1g231
// app.UseMiddleware<TokenMiddleware>();
app.MapGet("/", async (context) => {
    await context.Response.WriteAsync(@"
    <html>
        <body>
            <form method = 'post' action='/result'>
                <label for='value'>Enter value: </label>
                <input name='value' type='text><br>
                <label for='date'> Select date: </label>
                <input name='date' type='date'><br>
                <div>
                <button type='submit'>Submit</button>
                </div>
            </form>
            <div><a href='/cookies'>Check cookies</a></div>
        </body>
    </html>");
});
app.MapPost("/result", async context =>
{
    var value = context.Request.Form["value"];
    var date = context.Request.Form["date"];
    //context.Response.Cookies.Append("value", value);
    if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(date))
    {
        await context.Response.WriteAsync("Error! Value | Date are empty");
        logger.LogError("Error! Value | Date are empty");
        return;
    }
    if (DateTime.TryParse(date, out DateTime expiryDate))
    {
        if (DateTime.Parse(date) < DateTime.Now)
        {
            await context.Response.WriteAsync("Error! Date is expired");
            logger.LogError("Error! Date is expired");
        }
        else
        {
            context.Response.Cookies.Append("value" + Guid.NewGuid().ToString(), value, new CookieOptions
            {
                Expires = expiryDate
            });
            await context.Response.WriteAsync(@"
            <html>
                <body>
                    <h2>Value has been saved in Cookies!</h2>
                    <a href='/'>Home<a><br>
                </body>
            </html>");
        }
    }
    else
    {
        await context.Response.WriteAsync("Error");
        logger.LogError("Error");
    }
});
app.MapGet("/cookies", async context =>
{
    await context.Response.WriteAsync("<html><body>");
    foreach (var cookie in context.Request.Cookies)
    {
        await context.Response.WriteAsync($"<p>Key: {cookie.Key}: Value: {cookie.Value}</p>");
    }
    await context.Response.WriteAsync("<a href='/'>Back</a></body></html>");
});
app.Run();

