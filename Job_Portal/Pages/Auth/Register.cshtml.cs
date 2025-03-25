using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Models;

namespace Job_Portal.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel RegisterInput { get; set; }

        public void OnGet(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                RegisterInput = new RegisterViewModel { Email = email };
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // In a real application, this would create a new user account
            // For now, we'll just redirect to the home page
            TempData["Message"] = "Registration successful!";
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostSocialLogin(string provider)
        {
            // In a real application, this would handle social registration
            // For now, we'll just redirect to the home page
            TempData["Message"] = $"Registration with {provider} successful!";
            return RedirectToPage("/Index");
        }
    }
}

