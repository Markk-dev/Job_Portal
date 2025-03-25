using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Models;

namespace Job_Portal.Pages
{
    public class ProfileModel : PageModel
    {
        public UserProfile UserProfile { get; set; }

        [BindProperty]
        public bool IsResumeVisible { get; set; }

        public void OnGet()
        {
            // In a real application, this would be loaded from a database
            // For now, we'll use placeholder data
            UserProfile = new UserProfile
            {
                Id = 1,
                FullName = "User Name",
                Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut ea commodo consequat.",
                EmploymentStatus = "Student",
                Location = "City, Country (sample)",
                University = "XYZ State College (sample)",
                DegreeType = "Bachelor",
                ResumeFileName = "Professional Resume.pdf",
                IsResumeVisible = true
            };

            IsResumeVisible = UserProfile.IsResumeVisible;
        }

        public IActionResult OnPostUpdateResumeVisibility()
        {
            // In a real application, this would update the database
            // For now, we'll just redirect back to the page
            return RedirectToPage();
        }

        public IActionResult OnPostDownloadResume()
        {
            // In a real application, this would download the actual resume file
            // For now, we'll just redirect back to the page
            TempData["Message"] = "Resume download started!";
            return RedirectToPage();
        }
    }
}

