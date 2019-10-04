using System.Linq;
using AutoMapper;
using Site.Application.Addresses.Models;
using Site.Application.CareerExperience.Enums;
using Site.Application.CareerExperience.Models;
using Site.Application.Company.Models;
using Site.Application.Entities;
using Site.Application.GithubRepos.Models;
using Site.Application.Infrastructure.Models;
using Site.Application.Hobbies.Model;
using Site.Application.Projects.Model;
using Site.Application.Skills.Model;
using Site.Application.SocialMediaAccounts.Models;
using Site.Application.Technologies.Models;
using Site.Application.Users.Models;

namespace Site.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<BlogPost, BlogPostResponse>().ReverseMap();
            CreateMap<Technology, TechnologyDto>().ReverseMap();
            CreateMap<GithubRepo, GithubRepoDto>().ReverseMap();
            CreateMap<Entities.Company, CompanyDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<Technology, TechnologyDto>().ReverseMap();
            CreateMap<GithubRepo, GithubRepoApiResultDto>().ReverseMap();
            CreateMap<SocialMediaAccount, SocialMediaAccountDto>()
                .ForMember(x => x.Platform, y => y.MapFrom(z => z.SocialMediaPlatform.Name))
                .ReverseMap();
            CreateMap<Project, ProjectDto>()
                //.ForMember(x => x.Technologies, y => y.MapFrom(z => z.ProjectTechnologies.Select(x => x.TechnologyId)))
                //.ForMember(x => x.Skills, y => y.MapFrom(z => z.ProjectSkills.Select(x => x.SkillId)))
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(x => x.CurrentLocation, y => y.MapFrom(z => z.Address.City + ", " + z.Address.Country))
                .ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Hobby, HobbyDto>()
                .ForMember(x => x.Type, y => y.MapFrom(z => z.HobbyType.Type))
                .ReverseMap();
            CreateMap<Degree, DegreeDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<Degree, UserExperienceDto>()
                .ForMember(x => x.ExperienceType, cfg => cfg.MapFrom(x => ExperienceType.Degree))
                .ForMember(x => x.StartDate, cfg => cfg.MapFrom(x => x.StartDate))
                .ForMember(x => x.EndDate, cfg => cfg.MapFrom(x => x.EndDate))
                .ForMember(x => x.DegreeId, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Degree, cfg => cfg.MapFrom(x => x))
                .ReverseMap();
            CreateMap<Job, UserExperienceDto>()
                .ForMember(x => x.ExperienceType, cfg => cfg.MapFrom(x => ExperienceType.Job))
                .ForMember(x => x.StartDate, cfg => cfg.MapFrom(x => x.StartDate))
                .ForMember(x => x.EndDate, cfg => cfg.MapFrom(x => x.EndDate))
                .ForMember(x => x.JobId, cfg => cfg.MapFrom(x => x.Id))
                .ForMember(x => x.Job, cfg => cfg.MapFrom(x => x))
                .ReverseMap();
            CreateMap<University, UniversityDto>().ReverseMap();
            CreateMap<Grade, GradeDto>().ReverseMap();
        }
    }
}