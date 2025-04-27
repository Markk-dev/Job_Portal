using System.Collections.Generic;
using System.Linq;
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
        public int? SearchCompany { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchLocation { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchIndustries { get; set; }

        public void OnGet()
        {
            Companies = new List<Company>
            {
                new Company
                {
                    Id = 1,
                    Name = "Google",
                    Location = "United States",
                    LogoColor = "#4285F4",
                    Description = "Google is a global technology company specializing in search engines, cloud computing," +
                    " AI, and digital services. Founded in 1998 by Larry Page and Sergey Brin, it is best known for products" +
                    " like Google Search, Gmail, YouTube, and Android."
                },
                new Company
                {
                    Id = 2,
                    Name = "Microsoft",
                    Location = "Redmond, Washington, USA",
                    LogoColor = "#F25022",
                    Description = "Microsoft is a global technology company known for software, hardware, and cloud computing." +
                    " Founded in 1975 by Bill Gates and Paul Allen, it is best known for Windows OS, Microsoft Office," +
                    " Azure cloud services, and gaming (Xbox). The company is a leader in AI, enterprise solutions, and cybersecurity."
                },
                new Company
                {
                    Id = 3,
                    Name = "Apple",
                    Location = "Cupertino, California, USA",
                    LogoColor = "#A2AAAD",
                    Description = "Apple is a leading technology company specializing in consumer electronics, software, and digital services. " +
                    "Founded in 1976 by Steve Jobs, Steve Wozniak, and Ronald Wayne, Apple is best known for the iPhone, Mac computers, iPad, " +
                    "Apple Watch, and software like iOS and macOS. It is also a major player in digital services, including the App Store, " +
                    "Apple Music, and iCloud."
                }
            };

            // Select company based on dropdown value
            SelectedCompany = Companies.FirstOrDefault(c => c.Id == SearchCompany) ?? Companies[0];
        }
    }
}
