using System;
using Site.Application.Addresses.Models;
using Site.Application.GithubRepos.Models;

namespace Site.Application.Users.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string  CurrentLocation { get; set; }
        public string PersonalStatement { get; set; }
        
    }
}