using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Data;
using System.Text;
using System.Security.Cryptography;

namespace Job_Portal.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public LoginModel(ApplicationDbContext db) => _db = db;

    [BindProperty] public string Username { get; set; }
    [BindProperty] public string Password { get; set; }
    public string ErrorMessage { get; set; }

    public IActionResult OnPost()
    {
        //For Validation of Fields (make sure they are not empty)
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Field invalid";
            return Page();
        }

        var hashedPassword = HashPassword(Password);

        var user = _db.Users.FirstOrDefault(u => u.Username == Username && u.Password == hashedPassword);

        if (user == null)
        {
            ErrorMessage = "Invalid credentials. Please try again.";
            return Page();
        }

        HttpContext.Session.SetString("username", user.Username);
        return RedirectToPage("/Index");
    }

    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }
}
