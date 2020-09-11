using System.Collections.Generic;
using MediatR;
using Site.Application.Education.Model;

namespace Site.Application.Education.Queries
{
  public class GetUserEducationQuery : IRequest<List<UserDegreeDto>>
  { 
    public int UserId { get; set; }

    public GetUserEducationQuery(int userId)
    {
      UserId = userId;
    }
  }
}
