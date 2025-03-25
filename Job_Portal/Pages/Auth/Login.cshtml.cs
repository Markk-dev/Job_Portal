using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Models;

namespace Job_Portal.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel LoginInput { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // In a real application, this would validate credentials and sign in the user
            // For now, we'll just redirect to the home page
            TempData["Message"] = "Login successful!";
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostSocialLogin(string provider)
        {
            // In a real application, this would handle social login
            // For now, we'll just redirect to the home page
            TempData["Message"] = $"Login with {provider} successful!";
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostContinueWithEmail()
        {
            // In a real application, this would send a magic link or redirect to password entry
            // For now, we'll just redirect to the register page
            return RedirectToPage("/Auth/Register", new { email = LoginInput.Email });
        }
    }
}

