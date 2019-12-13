using System;
using System.Collections.Generic;
using Site.Application.CareerExperience.Enums;
using Site.Application.Company.Models;

namespace Site.Application.CareerExperience.Models
{
    public class UserExperienceDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentPosition => !EndDate.HasValue;
        public string Role { get; set; }
        public string Company { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Technologies { get; set; }
        public string  Location { get; set; }
        
    }
}
