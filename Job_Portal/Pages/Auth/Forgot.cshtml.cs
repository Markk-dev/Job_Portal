using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Models;
using Job_Portal.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Job_Portal.Pages.Auth
{
    public class ForgotModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public ForgotModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet()
        {
            // Just display the forgot password page.
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Please enter your email.";
                return Page();
            }

            Email = Email.Trim();

            var user = _db.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
            {
                ErrorMessage = "Email not found. Please check and try again.";
                return Page();
            }

            ErrorMessage = string.Empty;

            return RedirectToPage("/Auth/Login");
        }

        public async Task<JsonResult> OnPostCheckEmailAsync([FromBody] EmailCheckRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return new JsonResult(new { exists = false, message = "Please enter your email." });
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == request.Email.Trim());

            if (user == null)
            {
                return new JsonResult(new { exists = false, message = "Email not found" });
            }

            return new JsonResult(new { exists = true });
        }

        public async Task<JsonResult> OnPostUpdatePasswordAsync([FromBody] UpdatePasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return new JsonResult(new { success = false, message = "Invalid input." });
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == request.Email.Trim());

            if (user == null)
            {
                return new JsonResult(new { success = false, message = "User not found." });
            }

            // Validate password
            if (!IsValidPassword(request.NewPassword))
            {
                return new JsonResult(new { success = false, message = "Password must be at least 8 characters, contain only letters and numbers, and have no spaces." });
            }

            // Hash password
            var hashedPassword = HashPassword(request.NewPassword);

            // Update user's password
            user.Password = hashedPassword;
            await _db.SaveChangesAsync();

            return new JsonResult(new { success = true, message = "Password updated successfully!" });
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8) return false;

            // Only letters and numbers, no spaces or special characters
            var regex = new Regex(@"^[a-zA-Z0-9]+$");
            return regex.IsMatch(password);
        }
    }

    public class EmailCheckRequest
    {
        public string Email { get; set; }
    }

    public class UpdatePasswordRequest
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
