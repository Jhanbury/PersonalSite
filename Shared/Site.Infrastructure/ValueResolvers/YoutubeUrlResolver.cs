using AutoMapper;
using Google.Apis.YouTube.v3.Data;
using Site.Application.PlatformAccounts.Model;

namespace Site.Infrastructure.ValueResolvers
{
    public class YouTubeChannelUrlResolver : IValueResolver<Channel, PlatformAccount, string>
    {
        
        public string Resolve(Channel source, PlatformAccount destination, string destMember, ResolutionContext context)
        {
            return $"https://www.youtube.com/channel/{source.Id}";
        }
    }
}