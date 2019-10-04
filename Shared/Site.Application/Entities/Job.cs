using System;

namespace Site.Application.Entities
{
    public class Job 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Role { get; set; }
        public User User { get; set; }

    }
}