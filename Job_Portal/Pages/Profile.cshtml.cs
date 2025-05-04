using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Data;
using Job_Portal.Models;
using Microsoft.AspNetCore.Http;

namespace Job_Portal.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _db; 

        public UserProfile UserProfile { get; set; }

        [BindProperty]
        public bool IsResumeVisible { get; set; }

        public ProfileModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            // Fetch  username from session
            var username = HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(username))
            {
                
                RedirectToPage("/Auth/Login");
                return;
            }

           
            var user = _db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                
                RedirectToPage("/Error");
                return;
            }

            
            UserProfile = new UserProfile
            {
                Id = user.Id,
                FullName = user.Username,  
                Bio = "An aspiring IT student passionate about coding, problem-solving, and technology.",
                EmploymentStatus = "Student",
                Location = "LC, Philippines", 
                University = "St. Anne College Lucena Inc.",  
                DegreeType = "Bachelor",  
                ResumeFileName = "Professional Resume.pdf",  
                IsResumeVisible = true 
            };

         
            IsResumeVisible = UserProfile.IsResumeVisible;
        }

        public IActionResult OnPostUpdateResumeVisibility()
        {
          
            var username = HttpContext.Session.GetString("username");
            var user = _db.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                
                _db.SaveChanges();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDownloadResume()
        {

            TempData["Message"] = "Resume download started!";
            return RedirectToPage();
        }
    }
}
