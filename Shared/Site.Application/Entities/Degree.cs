using System;

namespace Site.Application.Entities
{
    public class Degree
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UniversityId { get; set; }
        public int GradeId { get; set; }
        public University University { get; set; }
        public Grade Grade { get; set; }
        public User User { get; set; }

    }
}