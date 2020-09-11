using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Interfaces;
using Site.Application.Videos.Models;
using Site.Domain.Entities;

namespace Site.Application.PlatformAccounts.Queries
{
  public class GetUserLiveStreamQueryHandler : IRequestHandler<GetUserLiveStreamsQuery, LiveStreamDto>
  {
    private readonly IRepository<PlatformAccount, int> _repository;
    private readonly IMapper _mapper;

    public GetUserLiveStreamQueryHandler(IRepository<PlatformAccount, int> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<LiveStreamDto> Handle(GetUserLiveStreamsQuery request, CancellationToken cancellationToken)
    {
      var accounts = await _repository.Get(x => x.IsLive);
      var platformAccounts = accounts.ToList();
      if (platformAccounts.Any())
      {
        var model = platformAccounts.FirstOrDefault();
        return _mapper.Map<LiveStreamDto>(model);
      }
      else
      {
        return null;
      }
    }
  }
}
