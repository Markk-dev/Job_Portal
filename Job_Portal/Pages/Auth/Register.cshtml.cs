using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Models;
using Job_Portal.Data;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Job_Portal.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

      
        public IActionResult OnPost()
        {
            // Check Fields if emtpy
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }

            // Sanitize inputs (remove trailings)
            Username = Username.Trim();
            Email = Email.Trim();

            // Check if Username or Email already exists
            if (_db.Users.Any(u => u.Username == Username))
            {
                ErrorMessage = "Username is already taken.";
                return Page();
            }

            if (_db.Users.Any(u => u.Email == Email))
            {
                ErrorMessage = "Email is already taken.";
                return Page();
            }

            //  Hash password
            var hashedPassword = HashPassword(Password);

            // Section for creating a new user
            var user = new User
            {
                Username = Username,
                Email = Email,
                Password = hashedPassword
            };

            // Database operation
            _db.Users.Add(user);
            _db.SaveChanges();

            //Route to Login page after successful registration
            return RedirectToPage("Login", new { area = "" });
        }

        // Method to hash the password using SHA-256
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
