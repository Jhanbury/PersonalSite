using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.CareerTimeLine.Models;
using Site.Application.Interfaces;
using Site.Domain.Entities;

namespace Site.Application.CareerTimeLine.Queries
{
  public class GetUserCareerTimelineQueryHandler : IRequestHandler<GetUserCareerTimelineQuery, List<CareerTimeLineDto>>
  {
    private readonly IRepository<UserCertification, int> _certRepository;
    private readonly IRepository<UserWorkExperience, int> _workExRepository;
    private readonly IRepository<UserDegree, int> _degreeRepository;
    private readonly IMapper _mapper;

    public GetUserCareerTimelineQueryHandler(IRepository<UserWorkExperience, int> workExRepository, IRepository<UserCertification, int> repository, IRepository<UserDegree, int> degreeRepository, IMapper mapper)
    {
      _mapper = mapper;
      _certRepository = repository;
      _workExRepository = workExRepository;
      _degreeRepository = degreeRepository;

    }

    

    public async Task<List<CareerTimeLineDto>> Handle(GetUserCareerTimelineQuery request, CancellationToken cancellationToken)
    {
      var userCertsTask = GetUserCerts(request.UserId);
      var userDegreesTask = GetUserDegrees(request.UserId);
      var userJobsTask = GetUserJobs(request.UserId);

      await Task.WhenAll(userCertsTask, userDegreesTask, userJobsTask);
      var userDegrees = userDegreesTask.Result;
      var userCerts = userCertsTask.Result;
      var userJobs = userJobsTask.Result;

      return userDegrees.Union(userCerts).Union(userJobs).OrderBy(x => x.Date).ToList();
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
    private async Task<IEnumerable<CareerTimeLineDto>> GetUserJobs(int id)
    {
      var degrees = await _workExRepository.GetIncluding(x => x.UserId.Equals(id), x => x.Role, x => x.Company, x => x.Company.Location);
      return degrees.Select(x => _mapper.Map<CareerTimeLineDto>(x)).OrderBy(x => x.Date).ToList();
    }
  }
}
