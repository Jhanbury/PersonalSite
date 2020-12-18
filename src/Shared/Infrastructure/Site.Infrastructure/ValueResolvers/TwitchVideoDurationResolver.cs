using System;
using AutoMapper;
using TwitchVideo = TwitchLib.Api.V5.Models.Videos.Video;
using SiteVideo = Site.Domain.Entities.Video;
namespace Site.Infrastructure.ValueResolvers
{
  public class TwitchVideoDurationResolver : IValueResolver<TwitchVideo, SiteVideo, string>
  {
    public string Resolve(TwitchVideo source, SiteVideo destination, string destMember, ResolutionContext context)
    {
      var lengthInSeconds = source.Length;
      var videoTimeSpan = TimeSpan.FromSeconds(lengthInSeconds);
      return videoTimeSpan.ToString(@"hh\:mm\:ss");
    }
  }
}
