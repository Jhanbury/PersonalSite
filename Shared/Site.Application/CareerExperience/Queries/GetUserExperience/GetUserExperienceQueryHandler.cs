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
        private readonly IRepository<Job, int> _jobRepository;
        private readonly IRepository<Degree, int> _degreeRepository;
        private readonly IMapper _mapper;

        public GetUserExperienceQueryHandler(IRepository<Job, int> repository, IRepository<Degree, int> drepository, IMapper mapper)
        {
            _jobRepository = repository;
            _degreeRepository = drepository;
            _mapper = mapper;
        }

        public async Task<List<UserExperienceDto>> Handle(GetUserExperienceQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetIncluding(x => x.UserId.Equals(request.UserId), j => j.Company);
            var degrees = await _degreeRepository.GetIncluding(x => x.UserId.Equals(request.UserId), d => d.Grade, d => d.University);
            var jobExps = jobs.Select(x => _mapper.Map<UserExperienceDto>(x));
            var degExps = degrees.Select(x => _mapper.Map<UserExperienceDto>(x));
            return jobExps.Union(degExps).OrderBy(x => x.StartDate).ToList();
        }

    }
}
