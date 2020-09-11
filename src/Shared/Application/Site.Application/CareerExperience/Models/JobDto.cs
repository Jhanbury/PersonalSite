using Site.Application.Company.Models;

namespace Site.Application.CareerExperience.Models
{
    public class JobDto
    {
        public CompanyDto Company { get; set; }
        public string Role { get; set; }
    }
}