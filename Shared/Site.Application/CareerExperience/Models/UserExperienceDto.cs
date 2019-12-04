using System;
using Site.Application.Company.Models;

namespace Site.Application.CareerExperience.Models
{
    public class UserExperienceDto
    {
        public int CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentPosition => !EndDate.HasValue;
        public CompanyDto Company { get; set; }
        
    }
}
