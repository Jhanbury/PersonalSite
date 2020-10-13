using MediatR;
using Site.Application.PlatformAccounts.Model;
using Site.Application.Videos.Models;

namespace Site.Application.PlatformAccounts.Queries
{
  public class GetUserLiveStreamsQuery : IRequest<LiveStreamDto>
  {
    public int UserId { get; set; }

    public GetUserLiveStreamsQuery(int userId)
    {
      UserId = userId;
    }
  }
}
