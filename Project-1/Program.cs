var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Creating an instance of a Company class.
var testCompany = new Company
{
    Name = "A-Company",
    Location = "Mykolaiv",
    Direction = "Mobile Development",
    Employeers = 13
};

// Printing an information about our instance.
app.MapGet("/", () =>
{
    var companyInformation = "Name: " + testCompany.Name + "| Location: " +
    testCompany.Location + "| Direction: " + testCompany.Direction
    + "| Employeers: " + testCompany.Employeers;
    return companyInformation;
});

// Getting the random value using Random().
app.MapGet("/randSomeValue", () =>
{
    var random = new Random();
    var randomValue = random.Next(0, 101);
    var result = "My random value = " + randomValue;
    return result;
});

// Using a middleware | Request (TOKEN).
app.Use(async (context, next) => {
    var token = context.Request.Query["token"];
    if (token == "1000") {
        await next();
    }
    else {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid token");
    }
});

// Run our app.
app.Run();

// Creating a Company class
public class Company {
    public string Name { get; set; }
    public string Location { get; set; }
    public string Direction { get; set; }
    public int Employeers { get; set; }
}