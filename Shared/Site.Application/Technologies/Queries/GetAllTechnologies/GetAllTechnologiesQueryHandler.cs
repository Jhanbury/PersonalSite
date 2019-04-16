using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Entities;
using Site.Application.Interfaces;
using Site.Application.Technologies.Models;

namespace Site.Application.Technologies.Queries.GetAllTechnologies
{
    public class GetAllTechnologiesQueryHandler : IRequestHandler<GetAllTechnologiesQuery, List<TechnologyDto>>
    {
        private readonly IRepository<Technology,int> _repository;
        private readonly IMapper _mapper;

        public GetAllTechnologiesQueryHandler(IRepository<Technology,int> repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<List<TechnologyDto>> Handle(GetAllTechnologiesQuery request, CancellationToken cancellationToken)
        {
            var technologies = await _repository.GetAll();
            return technologies.Select(x => _mapper.Map<TechnologyDto>(x)).ToList();
        }
    }
}