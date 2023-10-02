var builder = WebApplication.CreateBuilder();
var app = builder.Build();
builder.
    Configuration.
    AddXmlFile("Configurations/aboutApple.xml").
    AddJsonFile("Configurations/aboutGoogle.json").
    AddJsonFile("Configurations/userInformation.json").
    AddInMemoryCollection(new Dictionary<string, string> {
        {"password", "4000"},
    });

app.Map("/", (IConfiguration appConfig) => {
    var companyName = "";
    var employeesCount = 0;
    IConfigurationSection company = appConfig.GetSection("Company");
    foreach (var section in company.GetChildren()) {
        var currentName = section.Key;
        var currentEmployees = int.Parse(section.GetSection("employees").Value);

        if (currentEmployees > employeesCount) {
            companyName = currentName;
            employeesCount = currentEmployees;
        }
    }
    return $"Company: {companyName}. Employees count: {employeesCount}";
});

app.Map("/about", (IConfiguration appConfig) => {
    IConfigurationSection user = appConfig.GetSection("User");
    var firstName = user.GetSection("firstName").Value;
    var lastName = user.GetSection("lastName").Value;
    var age = user.GetSection("age").Value;
    var city = user.GetSection("city").Value;

    return $"First name: {firstName}. Last name: {lastName}. Age: {age}. " +
    $"City: {city}";
});

app.Run();