using AutoMapper;
using Site.Application.Addresses.Models;
using Site.Application.Entities;
using Site.Application.GithubRepos.Models;
using Site.Application.Technologies.Models;
using Site.Application.Users.Models;

namespace Site.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Technology, TechnologyDto>().ReverseMap();
            CreateMap<GithubRepo, GithubRepoDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
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