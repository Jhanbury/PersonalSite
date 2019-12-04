using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.CareerTimeLine.Models;
using Site.Application.Entities;
using Site.Application.Interfaces;

namespace Site.Application.CareerTimeLine.Queries
{
  public class GetUserCareerTimelineQueryHandler : IRequestHandler<GetUserCareerTimelineQuery, List<CareerTimeLineDto>>
  {
    private readonly IRepository<UserCertification, int> _certRepository;
    private readonly IRepository<UserDegree, int> _degreeRepository;
    private readonly IMapper _mapper;

    public GetUserCareerTimelineQueryHandler(IRepository<UserCertification, int> repository, IRepository<UserDegree, int> degreeRepository, IMapper mapper)
    {
      _mapper = mapper;
      _certRepository = repository;
      _degreeRepository = degreeRepository;

    }

    

    public async Task<List<CareerTimeLineDto>> Handle(GetUserCareerTimelineQuery request, CancellationToken cancellationToken)
    {
      var userCertsTask = GetUserCerts(request.UserId);
      var userDegreesTask = GetUserDegrees(request.UserId);

      await Task.WhenAll(userCertsTask, userDegreesTask);
      var userDegrees = userDegreesTask.Result;
      var userCerts = userCertsTask.Result;

      return userDegrees.Union(userCerts).OrderBy(x => x.Date).ToList();
    }


    private async Task<IEnumerable<CareerTimeLineDto>> GetUserCerts(int id)
    {
      var certifications = await _certRepository.GetIncluding(x => x.UserId.Equals(id), x => x.Certification, x => x.Certification.Accreditor);
      return certifications.Select(x => _mapper.Map<CareerTimeLineDto>(x)).OrderBy(x => x.Date).ToList();
    }

    private async Task<IEnumerable<CareerTimeLineDto>> GetUserDegrees(int id)
    {
      var degrees = await _degreeRepository.GetIncluding(x => x.UserId.Equals(id), x => x.Degree, x => x.Degree.University, x => x.Grade);
      return degrees.Select(x => _mapper.Map<CareerTimeLineDto>(x)).OrderBy(x => x.Date).ToList();
    }
  }
}
