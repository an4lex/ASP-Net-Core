using TestApplication.Interfaces;
using TestApplication.Services;
using System.Text;

var builder = WebApplication.CreateBuilder();
builder.Services
    .AddTransient<CalcServiceInterface, CalcService>()
    .AddTransient<TimeOfTheDayInterface, TimeOfTheDay>();

var app = builder.Build();

app.MapPost("/calculate", async context => {
    CalcServiceInterface? calcService = context.RequestServices.GetService<CalcServiceInterface>();
    var form = await context.Request.ReadFormAsync();
    var firstValue = int.Parse(form["firstValue"]);
    var secondValue = int.Parse(form["secondValue"]);
    var operation = form["operation"];
    var port = context.Request.Host.Port;
    float result = 0;

    switch (operation) {
        case "+":
            result = calcService.Sum(firstValue, secondValue);
            break;
        case "-":
            result = calcService.Subtract(firstValue, secondValue);
            break;
        case "*":
            result = calcService.Multiply(firstValue, secondValue);
            break;
        case "/":
            result = calcService.Divide(firstValue, secondValue);
            break;
    }

    context.Response.ContentType = "text/html;charset=utf-8";
    var responseHtml =
    $"<h2>Result = {result}</h2>" +
    $"<a href=https://localhost:{port}>Main Page</a>";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync(responseHtml);
});

app.MapGet("/", async context =>
{
    var stringBuilder = new StringBuilder();

    stringBuilder.Append($@"
    <div>
        <h1>Calculator</h1>
        <form method=""post"" action=""/calculate"">
            <div>
                <label for=""firstValue"">First value: </label>
                <input type=""number"" id=""firstValue"" name=""firstValue"" required>
            </div>
            <div>
                <label for=""operation"">Operation type: </label>
                <select id=""operation"" name=""operation"" required>
                    <option value=""+"">+</option>
                    <option value=""-"">-</option>
                    <option value=""*"">*</option>
                    <option value=""/"">/</option>
                </select>
            </div>
            <div>
                <label for=""secondValue"">Second value: </label>
                <input type=""number"" id=""secondValue"" name=""secondValue"" required>
            </div>
            <button type=""submit"">Get result</button>
        </form>
    </div>
");

    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync(stringBuilder.ToString());
});

app.MapGet("/time", async context =>
{
    TimeOfTheDayInterface? timeOfTheDay = context.RequestServices.GetService<TimeOfTheDayInterface>();
    string dayAppearance = timeOfTheDay.GetDayTime();
    string dayTime = timeOfTheDay.GetDayTime();
    var stringBuilder = new StringBuilder();
    stringBuilder.Append(
        $"<h2 style=\"color: {dayAppearance};\">" +
        $"{dayTime}</h2>");
    context.Response.ContentType = "text/html;charset=utf-8";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync(stringBuilder.ToString());
});

app.Run();