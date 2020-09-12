using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediatR;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;
using Site.Application.SocialMediaAccounts.Queries;

namespace Endpoints
{
    public class Endpoints
    {
        private readonly IMediator _mediator;

        public Endpoints(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("SocialLinks")]
        public async Task<IActionResult> SocialLinks([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{id}/sociallinks")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetUserSocialMediaAccountsQuery(id));

            return new OkObjectResult(result.ToList());
        }

        [FunctionName("GithubRepos")]
        public async Task<IActionResult> GithubRepos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{id}/repos")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetAllGithubReposQuery(id));

            return new OkObjectResult(result);
        }
    }
}
