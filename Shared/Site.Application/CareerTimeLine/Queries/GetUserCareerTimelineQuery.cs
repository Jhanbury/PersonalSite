using System.Collections.Generic;
using MediatR;
using Site.Application.CareerTimeLine.Models;

namespace Site.Application.CareerTimeLine.Queries
{
  public class GetUserCareerTimelineQuery : IRequest<List<CareerTimeLineDto>>
  {
    public int UserId { get; set; }

    public GetUserCareerTimelineQuery(int userId)
    {
      UserId = userId;
    }
  }
}
