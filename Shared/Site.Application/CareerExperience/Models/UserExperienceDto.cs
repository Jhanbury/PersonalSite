using System;
using Site.Application.CareerExperience.Enums;
using Site.Application.Company.Models;
using Site.Application.Entities;

namespace Site.Application.CareerExperience.Models
{
    public class UserExperienceDto
    {
        public int? JobId { get; set; }
        public int? DegreeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentPosition => !EndDate.HasValue;
        public JobDto Job { get; set; }
        public DegreeDto Degree { get; set; }
        public string ExperienceType { get; set; }
        
    }
}