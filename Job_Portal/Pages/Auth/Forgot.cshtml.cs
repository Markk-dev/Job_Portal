using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Job_Portal.Pages.Auth
{
    public class ForgotModel : PageModel
    {
        [BindProperty]
        public string UsernameOrEmail { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet()
        {
           
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(UsernameOrEmail))
            {
                ErrorMessage = "Please enter your username or email.";
                return Page();
            }

        

            return RedirectToPage("/Auth/Login");
        }
    }
}
