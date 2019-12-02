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
    private readonly IRepository<UserCertification, int> _repository;
    private readonly IMapper _mapper;

    public GetUserCareerTimelineQueryHandler(IRepository<UserCertification, int> repository, IMapper mapper)
    {
      _mapper = mapper;
      _repository = repository;

    }

    public async Task<List<CareerTimeLineDto>> Handle(GetUserCareerTimelineQuery request, CancellationToken cancellationToken)
    {
      var certifications = await _repository.GetIncluding(x => x.UserId.Equals(request.UserId), x => x.Certification, x => x.Certification.Accreditor);
      return certifications.Select(x => _mapper.Map<CareerTimeLineDto>(x)).OrderBy(x => x.Date).ToList();
    }
  }
}
