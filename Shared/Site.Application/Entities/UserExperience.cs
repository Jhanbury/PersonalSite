using System;

namespace Site.Application.Entities
{
    public class UserExperience
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentPosition => !EndDate.HasValue;
        public Company Company { get; set; }
        public User User { get; set; }
    }
}