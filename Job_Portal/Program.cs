using Job_Portal.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 29)))
);
builder.Services.AddSession();

var app = builder.Build();

// Changed the HTTP request pipeline (for HTTPS redirection and static files)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

// Redirect root path to /Auth/Login
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Community");
        return;
    }
    await next();
});

app.Use(async (context, next) =>
{
    var allowedPaths = new[]
    {
        "/Auth/Login",
        "/Auth/Register",
        "/Auth/Forgot"
    };

    var isAllowed = allowedPaths.Any(path =>
        context.Request.Path.StartsWithSegments(path, StringComparison.OrdinalIgnoreCase)
    );

    var isLoggedIn = context.Session.GetString("username") != null;

    if (!isAllowed && !isLoggedIn)
    {
        context.Response.Redirect("/Auth/Login");
        return;
    }

    await next();
});


app.MapRazorPages();

app.Run();
