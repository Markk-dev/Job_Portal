namespace Job_Portal.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public string EmploymentStatus { get; set; }
        public string Location { get; set; }
        public string University { get; set; }
        public string DegreeType { get; set; }
        public string ResumeFileName { get; set; }
        public bool IsResumeVisible { get; set; }
    }
}
