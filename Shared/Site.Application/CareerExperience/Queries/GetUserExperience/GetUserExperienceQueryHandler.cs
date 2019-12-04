using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.CareerExperience.Models;
using Site.Application.Interfaces;
using Site.Domain.Entities;

namespace Site.Application.CareerExperience.Queries
{
    public class GetUserExperienceQueryHandler : IRequestHandler<GetUserExperienceQuery, List<UserExperienceDto>>
    {
        private readonly IRepository<UserExperience, int> _repository;
        private readonly IMapper _mapper;

        public GetUserExperienceQueryHandler(IRepository<UserExperience, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserExperienceDto>> Handle(GetUserExperienceQuery request, CancellationToken cancellationToken)
        {
            var userExperiences = await _repository.GetIncluding(x => x.UserId.Equals(request.UserId),x => x.Company);
            return userExperiences.Select(x => _mapper.Map<UserExperienceDto>(x)).ToList();
        }

    }
}
