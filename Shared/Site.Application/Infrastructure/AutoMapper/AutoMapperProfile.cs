using AutoMapper;
using Site.Application.Addresses.Models;
using Site.Application.Entities;
using Site.Application.GithubRepos.Models;
using Site.Application.Infrastructure.Models;
using Site.Application.Hobbies.Model;
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
            CreateMap<GithubRepo, GithubRepoApiResultDto>().ReverseMap();
            CreateMap<SocialMediaAccount, SocialMediaAccountDto>()
                .ForMember(x => x.Platform, y => y.MapFrom(z => z.SocialMediaPlatform.Name))
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(x => x.CurrentLocation, y => y.MapFrom(z => z.Address.City + ", " + z.Address.Country))
                .ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Hobby, HobbyDto>()
                .ForMember(x => x.Type, y => y.MapFrom(z => z.HobbyType.Type))
                .ReverseMap();
        }

        //private void LoadConverters()
        //{

        //}

        //private void LoadStandardMappings()
        //{
        //    var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());

        //    foreach (var map in mapsFrom)
        //    {
        //        CreateMap(map.Source, map.Destination).ReverseMap();
        //    }
        //}

        //private void LoadCustomMappings()
        //{
        //    var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

        //    foreach (var map in mapsFrom)
        //    {
        //        map.CreateMappings(this);
        //    }
        //}
    }
}