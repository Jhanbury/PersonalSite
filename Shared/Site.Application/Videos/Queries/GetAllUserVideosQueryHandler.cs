using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Interfaces;
using Site.Application.Videos.Models;
using Site.Domain.Entities;

namespace Site.Application.Videos.Queries
{
    public class GetAllUserVideosQueryHandler : IRequestHandler<GetAllUserVideos, List<AccountDto>>
    {
        private readonly IRepository<PlatformAccount, int> _repository;
        private readonly IMapper _mapper;

        public GetAllUserVideosQueryHandler(IRepository<PlatformAccount, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AccountDto>> Handle(GetAllUserVideos request, CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetIncluding(x => x.UserId.Equals(request.UserId), x => x.Videos);
            var platformVideos = accounts.Select(x => _mapper.Map<AccountDto>(x));
            return platformVideos.ToList();
        }
    }
}
