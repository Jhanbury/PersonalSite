using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Interfaces;
using Site.Application.Users.Models;
using Site.Domain.Entities;

namespace Site.Application.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery,UserDto>
    {
        private readonly IRepository<User, int> _repository;
        private readonly IMapper _mapper;

        public GetUserInfoQueryHandler(IRepository<User, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetSingleIncluding(x => x.Id.Equals(request.UserId), x => x.Address);
            return _mapper.Map<UserDto>(user);
        }
    }
}
