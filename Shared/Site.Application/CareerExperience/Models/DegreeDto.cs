using System;

namespace Site.Application.CareerExperience.Models
{
    public class DegreeDto
    {
        //public int Id { get; set; }
        //public int UniversityId { get; set; }
        //public int GradeId { get; set; }
        public UniversityDto University { get; set; }
        public GradeDto Grade { get; set; }
    }
}