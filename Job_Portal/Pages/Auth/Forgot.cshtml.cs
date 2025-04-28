using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Models;
using Job_Portal.Data;
using System.Linq;


namespace Job_Portal.Pages.Auth
{
    public class ForgotModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ForgotModel(ApplicationDbContext db) => _db = db;
        [BindProperty]
        public string Email { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet() 
        {
           
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
    }

    public class EmailCheckRequest
    {
        public string Email { get; set; }
    }
}


