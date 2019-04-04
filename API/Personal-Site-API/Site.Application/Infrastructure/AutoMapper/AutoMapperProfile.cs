using AutoMapper;
using Site.Application.Entities;
using Site.Application.Technologies.Models;

namespace Site.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Technology, TechnologyDto>().ReverseMap();
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