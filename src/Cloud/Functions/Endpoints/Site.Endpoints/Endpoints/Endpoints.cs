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
using Site.Application.Articles.Querys.UserArticles;
using Site.Application.BlogPosts.Queries.GetUserBlogPosts;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;
using Site.Application.Hobbies.Querys;
using Site.Application.Infrastructure.Models.Twitch;
using Site.Application.Interfaces;
using Site.Application.PlatformAccounts.Queries;
using Site.Application.Projects.Queries;
using Site.Application.SocialMediaAccounts.Queries;
using Site.Application.Users.Queries;
using Site.Application.Users.Queries.GetUserInfo;
using Site.Application.Videos.Queries;

namespace Endpoints
{
    public class Endpoints
    {
        private readonly IMediator _mediator;
        private readonly ITwitchService _twitchService;

        public Endpoints(IMediator mediator, ITwitchService twitchService)
        {
            _mediator = mediator;
            _twitchService = twitchService;
        }

        [FunctionName("SocialLinks")]
        public async Task<IActionResult> SocialLinks([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}/social")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("----- Social Links Request Started -----");

            var result = await _mediator.Send(new GetUserSocialMediaAccountsQuery(id));
            
            return new CachedJsonResult(result.ToList());
        }

        [FunctionName("GithubRepos")]
        public async Task<IActionResult> GithubRepos([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}/repos")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetAllGithubReposQuery(id));

            return new CachedJsonResult(result);
        }

        [FunctionName("Projects")]
        public async Task<IActionResult> Projects([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}/projects")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetUserProjectsQuery(id));

            return new CachedJsonResult(result);
        }

        [FunctionName("About")]
        public async Task<IActionResult> About([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}/about")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetUserInfoQuery(id));

            return new CachedJsonResult(result);
        }

        [FunctionName("Blogs")]
        public async Task<IActionResult> Blogs([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}/blogs")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetUserBlogPostsQuery(id));

            return new CachedJsonResult(result);
        }

        [FunctionName("Articles")]
        public async Task<IActionResult> Articles([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}/articles")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("User Articles Query Started");

            var result = await _mediator.Send(new UserArticlesQuery(id));

            return new CachedJsonResult(result);
        }

        [FunctionName("Videos")]
        public async Task<IActionResult> Videos([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}/videos")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetAllUserVideos(id));

            return new CachedJsonResult(result);
        }

        [FunctionName("Hobbies")]
        public async Task<IActionResult> Hobbies([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}/hobbies")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetUserHobbiesQuery(id));

            return new CachedJsonResult(result);
        }

        [FunctionName("LiveStreams")]
        public async Task<IActionResult> LiveStreams([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}/livestreams")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _mediator.Send(new GetUserLiveStreamsQuery(id));

            return new CachedJsonResult(result);
        }

        

        [FunctionName("TwitchStream")]
        public async Task<IActionResult> TwitchStream([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "twitch/streamupdate/{userId}/{accountId}")] HttpRequest req, int userId, string accountId,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<TwitchStreamUpdateResponse>(requestBody);
            var commandData = _twitchService.HandleTwitchStreamUpdateWebhook(data, userId, accountId);
            var result = await _mediator.Send(commandData);

            return new OkObjectResult(result);
        }
    }
}
