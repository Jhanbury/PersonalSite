using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Application.GithubRepos.Models;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;

namespace Personal_Site_API.Controllers
{
    [Route("api/github-repos")]
    public class GithubReposController : BaseController
    {
        public GithubReposController(IMediator mediator, ICache cache, ILogger<GithubReposController> logger) : base(mediator, cache,logger)
        {
            
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var repos = await _cache.Method(x => x.Send<List<GithubRepoDto>>(new GetAllGithubReposQuery(), CancellationToken.None))
                    .ExpireAfter(TimeSpan.FromSeconds(5))
                    .GetValueAsync();
                return Ok(repos);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                return BadRequest();
            }
        }
    }
}