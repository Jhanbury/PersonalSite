using System;
using System.Collections.Generic;

namespace Site.Domains.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int ContactInformationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ContactInformation ContactInformation { get; set; }
        public ICollection<GithubRepo> GithubRepos { get; set; }
        public ICollection<PersonalProject> PersonalProjects { get; set; }
        public ICollection<SocialMediaAccount> SocialMediaAccounts { get; set; }
        
        
    }
}