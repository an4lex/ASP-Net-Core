using Microsoft.AspNetCore.Http;
using TestApplication.Other;
using System.Text;

// Create a web application builder
WebApplicationBuilder builder = WebApplication.CreateBuilder();
var app = builder.Build();

// Add a JSON configuration file
builder.Configuration.AddJsonFile("Configurations/library.json");

// Map a route for displaying student information by ID
app.Map("library/students/{id:int?}",
    async (int? id, HttpContext context, IConfiguration appConfig) => {

        // Retrieve the array of students from the configuration
        Student[] students = appConfig.GetSection("library:students").Get<Student[]>();
        StringBuilder sb = new StringBuilder();
        sb.Append("<div style='font-weight: medium;'>");

        // Check if an ID is provided and within valid bounds
        if (id.HasValue && students != null && id.Value >= 0 && id.Value < students.Length)
        {
            // Display student information if found
            sb.Append($"<p>Name: {students[id.Value].Name}. ID: {students[id.Value].StudentID}<p>");
        }
        else {
            // Display a message with user information if the ID is not found or invalid
            sb.Append($"<p>Name: {context.User}. Identity: {context.User.Identity}<p>");
        }

        sb.Append("</div>");
        await context.Response.WriteAsync(sb.ToString());
    });

// Map a route for the library's main page
app.Map("library", async (HttpContext context, IConfiguration appConfig) =>
{
    // Retrieve a greeting message from the configuration
    string greeting = appConfig.GetSection("greeting").Value;
    StringBuilder sb = new StringBuilder();
    sb.Append($"<div style='font-weight: medium;'>{greeting}</div>");
    await context.Response.WriteAsync(sb.ToString());
});

// Map a route for displaying a list of books in the library
app.Map("library/books", async(HttpContext context, IConfiguration appConfig) =>
{
    // Retrieve an array of books from the configuration
    Book[] books = appConfig.GetSection("library:books").Get<Book[]>();
    StringBuilder sb = new StringBuilder();
    sb.Append("<div style='font-weight: medium;'>");

    // Iterate through books and display their information
    foreach (var book in books) {
        sb.Append($"<p>Book: {book.Title} | Author: {book.Author}<p>");
    }

    sb.Append("</div>");
    await context.Response.WriteAsync(sb.ToString());
});

app.Map("/", async (HttpContext context, IConfiguration appConfig) =>
{
    // Retrieve the array of students from the configuration
    Student[] students = appConfig.GetSection("library:students").Get<Student[]>();
    StringBuilder sb = new StringBuilder();
    sb.Append($"<div style='font-weight'>" +
        $"<ul>" +
        $"<li><a href='/library'>Library</a></li>" +
        $"<li><a href='/library/books'>Search for book</a></li>");

    // Generate links to individual student pages
    for (int i = 0; i < students.Length; i++)
    {
        sb.Append($"<li><a href='/library/users/{i}'>{students[i].Name}</a></li>");
    }

    sb.Append($"</ul>" + "</div>");
    await context.Response.WriteAsync(sb.ToString());
});

// Use a middleware to handle 404 errors
app.Use(async (context, next) => {
    await next.Invoke();

    if (context.Response.StatusCode == 404)
        await context.Response.WriteAsync("Not Found");
});
app.Run();
