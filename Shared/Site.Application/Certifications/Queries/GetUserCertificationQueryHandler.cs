using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Certifications.Models;
using Site.Application.Entities;
using Site.Application.Interfaces;

namespace Site.Application.Certifications.Queries
{
  public class GetUserCertificationQueryHandler : IRequestHandler<GetUserCertificationsQuery, List<UserCertificationDto>>
  {
    private readonly IRepository<UserCertification, int> _repository;
    private readonly IMapper _mapper;

    public GetUserCertificationQueryHandler(IMapper mapper, IRepository<UserCertification, int> repository)
    {
      _mapper = mapper;
      _repository = repository;
    }


    public async Task<List<UserCertificationDto>> Handle(GetUserCertificationsQuery request, CancellationToken cancellationToken)
    {
      var certs = await _repository.GetIncluding(x => x.UserId.Equals(request.UserId), x => x.Certification, x => x.Certification.Accreditor);
      return certs.Select(x => _mapper.Map<UserCertificationDto>(x)).ToList();
    }
  }
}
