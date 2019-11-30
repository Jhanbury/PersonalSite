using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.CareerExperience.Models;
using Site.Application.Education.Model;
using Site.Application.Entities;
using Site.Application.Interfaces;

namespace Site.Application.Education.Queries
{
  public class GetUserEducationQueryHandler : IRequestHandler<GetUserEducationQuery, List<UserDegreeDto>>
  {
    private readonly IRepository<UserDegree, int> _repository;
    private readonly IMapper _mapper;

    public GetUserEducationQueryHandler(IRepository<UserDegree, int> repository, IMapper mapper)
    {
      _mapper = mapper;
      _repository = repository;
    }

    public async Task<List<UserDegreeDto>> Handle(GetUserEducationQuery request, CancellationToken cancellationToken)
    {
      var degrees = await _repository.GetIncluding(x => x.UserId.Equals(request.UserId), x => x.Degree,
        x => x.Degree.University, x => x.Grade);
      return degrees.Select(x => _mapper.Map<UserDegreeDto>(x)).ToList();
    } 
  }
}
