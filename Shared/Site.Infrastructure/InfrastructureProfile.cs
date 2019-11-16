using AutoMapper;
using Google.Apis.YouTube.v3.Data;
using Site.Application.PlatformAccounts.Model;
using Site.Application.Enums;
using Site.Infrastructure.ValueResolvers;

namespace Site.Infrastructure
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Channel, PlatformAccount>()
                .ForMember(x => x.Title, y => y.MapFrom(x => x.Snippet.Title))
                .ForMember(x => x.IconUrl, y => y.MapFrom(x => x.Snippet.Thumbnails.Default__.Url))
                .ForMember(x => x.Followers, y => y.MapFrom(x => x.Statistics.SubscriberCount))
                .ForMember(x => x.Link, y => y.MapFrom<YouTubeChannelUrlResolver>())
                .ForMember(x => x.PlatformId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Platform, y => y.MapFrom(x => Site.Application.Enums.Platform.YouTube ))
                .ForMember(x => x.Id, y => y.Ignore())
                .ReverseMap();

            CreateMap<Video, Site.Application.Videos.Models.Video>()
                .ForMember(x => x.SourceId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Title, y => y.MapFrom(x => x.Snippet.Title))
                .ForMember(x => x.ThumbnailUrl, y => y.MapFrom(x => x.Snippet.Thumbnails.Default__.Url))
                .ForMember(x => x.Url, y => y.MapFrom<YouTubeVideoUrlResolver>())
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.VideoDuration, y => y.MapFrom(x => x.ContentDetails.Duration))
                .ForMember(x => x.PublishDate, y => y.MapFrom(x => x.Snippet.PublishedAt))
                .ForMember(x => x.ViewCount, y => y.MapFrom(x => x.Statistics.ViewCount))
                .ReverseMap();
        }
    }
}