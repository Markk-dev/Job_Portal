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
        Id = 1,
        CompanyName = "AT&T",
        Title = "Retail Sales Associate",
        Location = "Lakewood, CA",
        MinSalary = 40000,
        MaxSalary = 60000,
        Overview = "BMR Business Solutions is a fast-growing small business in Orange, CA. We are a professional team with high standards. We are hiring for our Retail Sales and Customer Service department, looking for someone confident in their work ethic, quick to learn, adaptable, and eager to be rewarded for performance. As a Retail Sales and Customer Service Associate, you will introduce in-store promotions and educate customers on new technology, services, and product features."
    },
    new Job
    {
        Id = 2,
        CompanyName = "Wealth Security Financial",
        Title = "Health Insurance Agent",
        Location = "Downey, CA",
        MinSalary = 50000,
        MaxSalary = 70000,
        Overview = "We are seeking dedicated and motivated individuals who enjoy being part of an amazing team to help families secure their wealth. Full-time and part-time roles are available."
    },
    new Job
    {
        Id = 3,
        CompanyName = "Thrive Los Angeles Inc.",
        Title = "Junior Account Executive",
        Location = "El Segundo, CA",
        MinSalary = 60000,
        MaxSalary = 80000,
        Overview = "Thrive LA Inc. is looking for a Junior Account Executive to join our sales and marketing team. We focus on building and maintaining strong business customer relationships. Our integrity and commitment to client growth have opened opportunities for market expansion both nationally and internationally."
    },
    new Job
    {
        Id = 4,
        CompanyName = "Google",
        Title = "Software Engineer",
        Location = "Mountain View, CA",
        MinSalary = 120000,
        MaxSalary = 180000,
        Overview = "Join Google engineering team to create innovative products that impact billions of users around the world."
    },
    new Job
    {
        Id = 5,
        CompanyName = "Amazon",
        Title = "Operations Manager",
        Location = "Seattle, WA",
        MinSalary = 90000,
        MaxSalary = 130000,
        Overview = "Lead and optimize fulfillment operations, ensuring high efficiency and excellent customer satisfaction."
    },
    new Job
    {
        Id = 6,
        CompanyName = "Tesla",
        Title = "Mechanical Design Engineer",
        Location = "Fremont, CA",
        MinSalary = 95000,
        MaxSalary = 140000,
        Overview = "Design and develop cutting-edge automotive components for next generation of electric vehicles."
    },
    new Job
    {
        Id = 7,
        CompanyName = "Meta Platforms",
        Title = "Product Manager",
        Location = "Menlo Park, CA",
        MinSalary = 110000,
        MaxSalary = 160000,
        Overview = "Shape the product vision and lead execution for social platforms while working with cross-functional teams."
    },
    new Job
    {
        Id = 8,
        CompanyName = "Netflix",
        Title = "Content Analyst",
        Location = "Los Gatos, CA",
        MinSalary = 80000,
        MaxSalary = 115000,
        Overview = "Analyze and manage content for global audience, using data insights to guide programming choices."
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

