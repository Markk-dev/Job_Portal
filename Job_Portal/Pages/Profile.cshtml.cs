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
                Bio = "An aspiring IT student passionate about coding, problem-solving, and technology." +
                " Constantly learning and exploring new innovations in software development and cybersecurity",
                EmploymentStatus = "Student",
                Location = "LC, Phiippines",
                University = "St. Anne College Lucena Inc.",
                DegreeType = "Bachelor",
                ResumeFileName = "Professional Resume.pdf",
                IsResumeVisible = true
            };

            IsResumeVisible = UserProfile.IsResumeVisible;
        }

        public IActionResult OnPostUpdateResumeVisibility()
        {
          
            return RedirectToPage();
        }

        public IActionResult OnPostDownloadResume()
        {
           
            TempData["Message"] = "Resume download started!";
            return RedirectToPage();
        }
    }
}

