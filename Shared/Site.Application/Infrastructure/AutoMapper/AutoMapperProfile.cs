using System.Linq;
using AutoMapper;
using Site.Application.Addresses.Models;
using Site.Application.CareerExperience.Models;
using Site.Application.CareerTimeLine.Models;
using Site.Application.Certifications.Models;
using Site.Application.Company.Models;
using Site.Application.Education.Model;
using Site.Application.Entities;
using Site.Application.GithubRepos.Models;
using Site.Application.Infrastructure.Models;
using Site.Application.Hobbies.Model;
using Site.Application.PlatformAccounts.Model;
using Site.Application.Projects.Model;
using Site.Application.Skills.Model;
using Site.Application.SocialMediaAccounts.Models;
using Site.Application.Technologies.Models;
using Site.Application.Users.Models;
using Site.Application.Videos.Models;

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
            CreateMap<UserExperience, UserExperienceDto>().ReverseMap();
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
            CreateMap<Video, VideoDto>()
                .ReverseMap();
            CreateMap<PlatformAccount, AccountDto>()
                .ReverseMap();
            CreateMap<Certification, CertificationDto>()
              .ForMember(x => x.Accreditor, cfg => cfg.MapFrom(x => x.Accreditor.Name))
              .ReverseMap();
            CreateMap<UserCertification, UserCertificationDto>()
              .ForMember(x => x.Accreditor, cfg => cfg.MapFrom(x => x.Certification.Accreditor.Name))
              .ForMember(x => x.CertificationName, cfg => cfg.MapFrom(x => x.Certification.Name))
              .ForMember(x => x.CertificationDescription, cfg => cfg.MapFrom(x => x.Certification.Description))
              .ReverseMap();
            CreateMap<UserDegree, UserDegreeDto>()
              .ForMember(x => x.University, cfg => cfg.MapFrom(x => x.Degree.University.Name))
              .ForMember(x => x.Grade, cfg => cfg.MapFrom(x => x.Grade.DisplayName))
              .ForMember(x => x.Degree, cfg => cfg.MapFrom(x => x.Degree.Title))
              .ReverseMap();

            CreateMap<UserCertification, CareerTimeLineDto>()
              .ForMember(x => x.Title, cfg => cfg.MapFrom(x => x.Certification.Name))
              .ForMember(x => x.SubTitle, cfg => cfg.MapFrom(x => x.Certification.Accreditor.Name))
              .ForMember(x => x.Date, cfg => cfg.MapFrom(x => x.DateObtained))
              .ForMember(x => x.TimeLineType, cfg => cfg.MapFrom(x => TimeLineType.Certification))
              //.ForMember(x => x., cfg => cfg.MapFrom(x => TimeLineType.Certification))
              .ReverseMap();
        }
    }
}
