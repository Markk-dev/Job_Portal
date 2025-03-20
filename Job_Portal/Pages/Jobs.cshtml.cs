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
            // Initialize sample job data
            Jobs = new List<Job>
            {
                new Job
                {
                    Id = 1,
                    CompanyName = "Company Inc",
                    Title = "Job title here",
                    Location = "Job Location",
                    MinSalary = 40000,
                    MaxSalary = 60000,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non suscipit tempus nisi dictum et bibendum " +
                              "at diam magna. Donec et ipsum viverra, quis malesuada sem at, varius diam et dui. Sed ac " +
                              "augue nulla. Curabitur a ante non lectus vehicula ultricies. Sed et ante felis nunc. Ut sit " +
                              "amet nulla placerat. Donec nec venenatis malesuada nec hendrerit ut in dictum nisi. Ut sit " +
                              "amet vel accumsan."
                },
                new Job
                {
                    Id = 2,
                    CompanyName = "Company Inc",
                    Title = "Job title here",
                    Location = "Job Location",
                    MinSalary = 50000,
                    MaxSalary = 70000,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non suscipit tempus nisi dictum et bibendum " +
                              "at diam magna. Donec et ipsum viverra, quis malesuada sem at, varius diam et dui."
                },
                new Job
                {
                    Id = 3,
                    CompanyName = "Company Inc",
                    Title = "Job title here",
                    Location = "Job Location",
                    MinSalary = 60000,
                    MaxSalary = 80000,
                    Overview = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non suscipit tempus nisi dictum et bibendum " +
                              "at diam magna. Donec et ipsum viverra, quis malesuada sem at, varius diam et dui."
                }
            };

            // Set the selected job (default to the first one if not specified)
            if (selectedJobId.HasValue)
            {
                SelectedJob = Jobs.Find(j => j.Id == selectedJobId.Value);
            }

            if (SelectedJob == null)
            {
                SelectedJob = Jobs[0];
            }

            // Mark the selected job
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
            // In a real application, this would handle the job application
            // For now, just redirect back to the page with a message
            TempData["Message"] = "Application submitted successfully!";
            return RedirectToPage(new { selectedJobId = jobId });
        }
    }
}

