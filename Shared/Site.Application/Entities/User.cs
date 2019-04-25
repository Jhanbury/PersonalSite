using System;
using System.Collections.Generic;

namespace Site.Application.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PersonalStatement { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<GithubRepo> GithubRepos { get; set; }
        public ICollection<SocialMediaAccount> SocialMediaAccounts { get; set; }
        public ICollection<UserBlogPost> UserBlogPosts { get; set; }
        public ICollection<UserHobby> UserHobbies { get; set; }
        
        
    }
}