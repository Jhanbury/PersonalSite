using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Entities;
using Site.Application.Hobbies.Model;
using Site.Application.Interfaces;

namespace Site.Application.Hobbies.Querys
{
    public class GetUserHobbiesQueryHandler : IRequestHandler<GetUserHobbiesQuery, List<HobbyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<UserHobby, int> _repository;

        public GetUserHobbiesQueryHandler(IMapper mapper, IRepository<UserHobby, int> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<HobbyDto>> Handle(GetUserHobbiesQuery request, CancellationToken cancellationToken)
        {
            var hobbys = await _repository.GetIncluding(user => user.UserId.Equals(request.Id),x => x.Hobby, x => x.Hobby.HobbyType);
            return hobbys.Select(x => x.Hobby)
                .Select(x => _mapper.Map<HobbyDto>(x))
                .ToList();
        }
    }
}