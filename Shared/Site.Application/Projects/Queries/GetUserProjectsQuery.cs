using System.Collections.Generic;
using MediatR;
using Site.Application.Projects.Model;

namespace Site.Application.Projects.Queries
{
    public class GetUserProjectsQuery : IRequest<List<ProjectDto>>
    {
        public GetUserProjectsQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}
