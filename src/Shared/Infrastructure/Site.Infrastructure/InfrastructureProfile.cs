using System.Linq;
using AutoMapper;
using Site.Domain.Entities;
using Site.Infrastructure.Models.Blogs;
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

            CreateMap<DevtoBlogApiResponse, UserBlogPost>()
              .ForMember(x => x.Title, cfg => cfg.MapFrom(y => y.Title))
              .ForMember(x => x.SourceId, cfg => cfg.MapFrom(y => y.Id.ToString()))
              .ForMember(x => x.Source, cfg => cfg.MapFrom(y => BlogSite.DevTo))
              .ForMember(x => x.Teaser, cfg => cfg.MapFrom(y => y.Description))
              .ForMember(x => x.PublishDate, cfg => cfg.MapFrom(y => y.PublishedAt))
              .ForMember(x => x.Likes, cfg => cfg.MapFrom(y => y.PositiveReactionsCount))
              .ForMember(x => x.Views, cfg => cfg.MapFrom(y => y.PageViewsCount))
              .ForMember(x => x.Comments, cfg => cfg.MapFrom(y => y.CommentsCount))
              .ForMember(x => x.Url, cfg => cfg.MapFrom(y => y.Url))
              .ForMember(x => x.ImageUrl, cfg => cfg.MapFrom(y => y.CoverImage))
              .ForMember(x => x.BlogPostTags, cfg => cfg.MapFrom(y => y.TagList.Select(x => new BlogPostTag(){ Tag = x})))
              .ReverseMap();
        }
    }
}
