using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Models;

namespace Job_Portal.Pages
{
    public class CompaniesModel : PageModel
    {
        public List<Company> Companies { get; set; } = new List<Company>();
        public Company SelectedCompany { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchCompany { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchLocation { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchIndustries { get; set; }

        public void OnGet(int? selectedCompanyId = null)
        {
            // Initialize sample company data
            Companies = new List<Company>
            {
                new Company
                {
                    Id = 1,
                    Name = "Company Name",
                    Location = "Location",
                    LogoColor = "#4CAF50", // Green
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et " +
                                 "dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip " +
                                 "ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore " +
                                 "fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt " +
                                 "mollit anim id est laborum."
                },
                new Company
                {
                    Id = 2,
                    Name = "Tech Solutions Inc",
                    Location = "San Francisco, CA",
                    LogoColor = "#2196F3", // Blue
                    Description = "Tech Solutions Inc is a leading technology company specializing in software development, cloud computing, " +
                                 "and artificial intelligence solutions. With a team of experienced professionals, we deliver innovative " +
                                 "products and services to clients worldwide."
                },
                new Company
                {
                    Id = 3,
                    Name = "Green Energy Co",
                    Location = "Portland, OR",
                    LogoColor = "#8BC34A", // Light Green
                    Description = "Green Energy Co is committed to sustainable energy solutions. We develop and implement renewable energy " +
                                 "technologies that help businesses and communities reduce their carbon footprint and achieve their " +
                                 "sustainability goals."
                }
            };

            // Set the selected company (default to the first one if not specified)
            if (selectedCompanyId.HasValue)
            {
                SelectedCompany = Companies.Find(c => c.Id == selectedCompanyId.Value);
            }

            if (SelectedCompany == null)
            {
                SelectedCompany = Companies[0];
            }
        }

        public IActionResult OnGetSelectCompany(int companyId)
        {
            return RedirectToPage(new { selectedCompanyId = companyId });
        }
    }
}

