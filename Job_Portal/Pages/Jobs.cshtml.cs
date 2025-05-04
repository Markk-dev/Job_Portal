using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Models;

namespace Job_Portal.Pages
{
    public class JobsModel : PageModel
    {
        public List<Job> Jobs { get; set; } = new List<Job>();
        public Job SelectedJob { get; set; }

        public void OnGet(int? selectedJobId = null)
        {
           
            Jobs = new List<Job>
            {
                new Job
                {
                    //Placeholder data for now
                    Id = 1,
                    CompanyName = "AT&T",
                    Title = "Retail Sales Associate",
                    Location = "Lakewood, CA",
                    MinSalary = 40000,
                    MaxSalary = 60000,
                    Overview = "BMR Business Solutions is young and aggressively growing small business in Orange," +
                    " CA. We are a professional team with very high standards.\r\n\r\nWe are hiring for our Retail Sales" +
                    " and Customer Service department and searching for someone who is confident in their work ethic, ability " +
                    "to learn and adapt to new challenges, and wants to be rewarded for their performance.\r\n\r\nAs a Retail" +
                    " Sales and Customer Service Associate you will be responsible for introducing in store promotions and " +
                    "educating customers on new technology, services, and product features."
                },
                new Job
                {
                    Id = 2,
                    CompanyName = "Wealth Security Financial",
                    Title = "Health Insurance Agent",
                    Location = "Downey, CA",
                    MinSalary = 50000,
                    MaxSalary = 70000,
                    Overview = "We are seeking a dedicated and motivated individuals who love being apart of an amazing team" +
                    " to help families secure their wealth!! Full time and Part time opportunities are available!!"
                },
                new Job
                {
                    Id = 3,
                    CompanyName = "Thrive Los Angeles Inc.",
                    Title = "Junior Account Executive",
                    Location = "El Segundo, CA",
                    MinSalary = 60000,
                    MaxSalary = 80000,
                    Overview = "Thrive LA Inc. is seeking a Junior Account Executive to join our team! We are a sales and marketing " +
                    "firm that excels in attaining, growing, and maintaining our client’s business customer bases. We hold the highest " +
                    "integrity with our clients and value building genuine relationships to connect them and their target markets directly. " +
                    "Due to driving outstanding results, we now have opportunities to continue market expansion for our clients nationally " +
                    "and internationally."
                }
            };

            if (selectedJobId.HasValue)
            {
                SelectedJob = Jobs.Find(j => j.Id == selectedJobId.Value);
            }

            if (SelectedJob == null)
            {
                SelectedJob = Jobs[0];
            }

           
            foreach (var job in Jobs)
            {
                job.IsSelected = job.Id == SelectedJob.Id;
            }
        }

        public IActionResult OnGetSelectJob(int jobId)
        {
            return RedirectToPage(new { selectedJobId = jobId });
        }

        public IActionResult OnPostApply(int jobId)
        {
            TempData["Message"] = "Application submitted successfully!";
            return RedirectToPage(new { selectedJobId = jobId });
        }
    }
}

