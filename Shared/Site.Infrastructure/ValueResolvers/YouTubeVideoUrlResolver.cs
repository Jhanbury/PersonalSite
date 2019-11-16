using AutoMapper;
using Google.Apis.YouTube.v3.Data;

namespace Site.Infrastructure.ValueResolvers
{
    public class YouTubeVideoUrlResolver : IValueResolver<Video,Site.Application.Videos.Models.Video,string>
    {
        public string Resolve(Video source, Application.Videos.Models.Video destination, string destMember, ResolutionContext context)
        {
            return $"https://www.youtube.com/watch?v={source.Id}";
        }
    }
}