using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Entities;
using Site.Application.Interfaces;
using Site.Application.SocialMediaAccounts.Models;

namespace Site.Application.SocialMediaAccounts.Queries
{
    public class GetUserSocialMediaAccountsQueryHandler : IRequestHandler<GetUserSocialMediaAccountsQuery,IEnumerable<SocialMediaAccountDto>>
    {
        private readonly IRepository<SocialMediaAccount, int> _repository;
        private readonly IMapper _mapper;

        public GetUserSocialMediaAccountsQueryHandler(IRepository<SocialMediaAccount, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SocialMediaAccountDto>> Handle(GetUserSocialMediaAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetIncluding(x => x.UserId.Equals(request.UserId), x => x.SocialMediaPlatform);
            return accounts.Select(x => _mapper.Map<SocialMediaAccountDto>(x));
        }
    }
}