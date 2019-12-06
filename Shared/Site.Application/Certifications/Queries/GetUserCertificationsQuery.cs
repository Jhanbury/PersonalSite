using System.Collections.Generic;
using MediatR;
using Site.Application.Certifications.Models;

namespace Site.Application.Certifications.Queries
{
  public class GetUserCertificationsQuery : IRequest<List<UserCertificationDto>>
  {
    public int UserId { get; set; }

    public GetUserCertificationsQuery(int userId)
    {
      UserId = userId;
    }
  }
}
