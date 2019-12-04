using AutoMapper;
using Site.Domain.Entities;
using Site.Infrastructure.ValueResolvers;
using Video = Google.Apis.YouTube.v3.Data.Video;

namespace Site.Infrastructure
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Google.Apis.YouTube.v3.Data.Channel, PlatformAccount>()
                .ForMember(x => x.Title, y => y.MapFrom(x => x.Snippet.Title))
                .ForMember(x => x.IconUrl, y => y.MapFrom(x => x.Snippet.Thumbnails.Default__.Url))
                .ForMember(x => x.Followers, y => y.MapFrom(x => x.Statistics.SubscriberCount))
                .ForMember(x => x.Link, y => y.MapFrom<YouTubeChannelUrlResolver>())
                .ForMember(x => x.PlatformId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Platform, y => y.MapFrom(x => Site.Domain.Enums.Platform.YouTube ))
                .ForMember(x => x.Id, y => y.Ignore())
                .ReverseMap();

            CreateMap<Video, Site.Domain.Entities.Video>()
                .ForMember(x => x.SourceId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Title, y => y.MapFrom(x => x.Snippet.Title))
                .ForMember(x => x.ThumbnailUrl, y => y.MapFrom(x => x.Snippet.Thumbnails.Default__.Url))
                .ForMember(x => x.Url, y => y.MapFrom<YouTubeVideoUrlResolver>())
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.VideoDuration, y => y.MapFrom(x => x.ContentDetails.Duration))
                .ForMember(x => x.PublishDate, y => y.MapFrom(x => x.Snippet.PublishedAt))
                .ForMember(x => x.ViewCount, y => y.MapFrom(x => x.Statistics.ViewCount))
                .ReverseMap();

            CreateMap<TwitchLib.Api.V5.Models.Channels.Channel, PlatformAccount>()
                .ForMember(x => x.Title, y => y.MapFrom(x => x.DisplayName))
                .ForMember(x => x.IconUrl, y => y.MapFrom(x => x.Logo))
                .ForMember(x => x.Followers, y => y.MapFrom(x => x.Followers))
                .ForMember(x => x.Link, y => y.MapFrom(x => x.Url))
                .ForMember(x => x.PlatformId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Platform, y => y.MapFrom(x => Site.Domain.Enums.Platform.Twitch))
                .ForMember(x => x.Id, y => y.Ignore())
                .ReverseMap();

            CreateMap<TwitchLib.Api.V5.Models.Videos.Video, Site.Domain.Entities.Video>()
                .ForMember(x => x.SourceId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Title, y => y.MapFrom(x => x.Title))
                .ForMember(x => x.ThumbnailUrl, y => y.MapFrom(x => x.Thumbnails.Large[0].Url))
                .ForMember(x => x.Url, y => y.MapFrom(x => x.Url))
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.VideoDuration, y => y.MapFrom(x => x.Length))
                .ForMember(x => x.PublishDate, y => y.MapFrom(x => x.PublishedAt))
                .ForMember(x => x.ViewCount, y => y.MapFrom(x => x.Views))
                .ReverseMap();
        }
    }
}
