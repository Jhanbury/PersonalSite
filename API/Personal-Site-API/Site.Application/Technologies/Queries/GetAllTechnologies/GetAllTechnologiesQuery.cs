using System.Collections.Generic;
using MediatR;
using Site.Application.GithubRepos.Models;
using Site.Application.Technologies.Models;

namespace Site.Application.Technologies.Queries.GetAllTechnologies
{
    public class GetAllTechnologiesQuery : IRequest<TechnologyDto>, IRequest<List<TechnologyDto>>
    {
        
    }
}