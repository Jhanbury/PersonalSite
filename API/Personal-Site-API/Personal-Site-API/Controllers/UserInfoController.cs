using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Application.BlogPosts.Models;
using Site.Application.BlogPosts.Queries.GetUserBlogPosts;
using Site.Application.CareerExperience.Queries;
using Site.Application.CareerTimeLine.Queries;
using Site.Application.Certifications.Models;
using Site.Application.Certifications.Queries;
using Site.Application.Education.Queries;
using Site.Application.GithubRepos.Models;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;
using Site.Application.Hobbies.Model;
using Site.Application.Hobbies.Querys;
using Site.Application.Interfaces;
using Site.Application.Projects.Queries;
using Site.Application.SocialMediaAccounts.Models;
using Site.Application.SocialMediaAccounts.Queries;
using Site.Application.Users.Models;
using Site.Application.Users.Queries;
using Site.Application.Videos.Models;
using Site.Application.Videos.Queries;

namespace Personal_Site_API.Controllers
{
    [Route("api/userinfo")]
    public class UserInfoController : BaseController
    {
        
        public UserInfoController(IMediator mediator, ICache cache, ILogger<UserInfoController> logger) : base(mediator, cache, logger)
        {
            
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult> GetUserInfo(int userId)
        {
            try
            {
                var userInfo = await _cache.Method(x =>  x.Send<UserDto>(new GetUserInfoQuery(userId), CancellationToken.None))
                    .ExpireAfter(TimeSpan.FromSeconds(5))
                    .GetValueAsync();
                return Ok(userInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{userId}/SocialMediaAccounts")]
        public async Task<ActionResult> GetUserSocialMediaAccounts(int userId)
        {
            try
            {
                var userInfo = await _cache.Method(x =>  x.Send<IEnumerable<SocialMediaAccountDto>>(new GetUserSocialMediaAccountsQuery(userId), CancellationToken.None))
                    .ExpireAfter(TimeSpan.FromSeconds(5))
                    .GetValueAsync();
                return Ok(userInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{userId}/Hobbies")]
        public async Task<ActionResult> GetUserHobbies(int userId)
        {
            try
            {
                var userInfo = await _cache.Method(x => x.Send<List<HobbyDto>>(new GetUserHobbiesQuery(userId), CancellationToken.None))
                    .ExpireAfter(TimeSpan.FromSeconds(5))
                    .GetValueAsync();
                return Ok(userInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{userId}/BlogPosts")]
        public async Task<ActionResult> GetUserBlogPosts(int userId)
        {
            try
            {
                var blogPosts = await _cache.Method(x => x.Send<List<UserBlogPostDto>>(new GetUserBlogPostsQuery(userId), CancellationToken.None))
                    .ExpireAfter(TimeSpan.FromSeconds(5))
                    .GetValueAsync();
                return Ok(blogPosts);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("{userId}/Projects")]
        public async Task<ActionResult> GetUserProjects(int userId)
        {
            try
            {
                
                var projects = await _cache.Method(x => x.Send(new GetUserProjectsQuery(userId), CancellationToken.None))
                    .ExpireAfter(TimeSpan.FromSeconds(5))
                    .GetValueAsync();
                return Ok(projects);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{userId}/Experience")]
        public async Task<ActionResult> GetUserExperience(int userId)
        {
            try
            {
                var experience = await _cache.Method(x => x.Send(new GetUserExperienceQuery(userId), CancellationToken.None))
                    .ExpireAfter(TimeSpan.FromSeconds(5))
                    .GetValueAsync();
                return Ok(experience);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{userId}/Videos")]
        public async Task<ActionResult> GetAllUserVideos(int userId)
        {
            try
            {
                var videos = await _cache.Method(x => x.Send<List<AccountDto>>(new GetAllUserVideos(userId), CancellationToken.None))
                    .ExpireAfter(TimeSpan.FromSeconds(5))
                    .GetValueAsync();
                return Ok(videos);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }

    [HttpGet]
    [Route("{userId}/codeRepos")]
    public async Task<ActionResult> GetRepos(int userId)
    {
      try
      {
        var repos = await _cache.Method(x => x.Send<List<GithubRepoDto>>(new GetAllGithubReposQuery(userId), CancellationToken.None))
            .ExpireAfter(TimeSpan.FromSeconds(5))
            .GetValueAsync();
        return Ok(repos);
      }
      catch (Exception e)
      {
        _logger.LogError(e, e.Message);
        return BadRequest();
      }
    }

    [HttpGet]
    [Route("{userId}/certifications")]
    public async Task<ActionResult> GetCerts(int userId)
    {
      try
      {
        var repos = await _cache.Method(x => x.Send(new GetUserCertificationsQuery(userId), CancellationToken.None))
          .ExpireAfter(TimeSpan.FromSeconds(5))
          .GetValueAsync();
        return Ok(repos);
      }
      catch (Exception e)
      {
        _logger.LogError(e, e.Message);
        return BadRequest();
      }
    }

    [HttpGet]
    [Route("{userId}/education")]
    public async Task<ActionResult> GetEducation(int userId)
    {
      try
      {
        var repos = await _cache.Method(x => x.Send(new GetUserEducationQuery(userId), CancellationToken.None))
          .ExpireAfter(TimeSpan.FromSeconds(5))
          .GetValueAsync();
        return Ok(repos);
      }
      catch (Exception e)
      {
        _logger.LogError(e, e.Message);
        return BadRequest();
      }
    }

    [HttpGet]
    [Route("{userId}/careertimeline")]
    public async Task<ActionResult> GetCareerTimeLine(int userId)
    {
      try
      {
        var repos = await _cache.Method(x => x.Send(new GetUserCareerTimelineQuery(userId), CancellationToken.None))
          .ExpireAfter(TimeSpan.FromSeconds(5))
          .GetValueAsync();
        return Ok(repos);
      }
      catch (Exception e)
      {
        _logger.LogError(e, e.Message);
        return BadRequest();
      }
    }
  }
}
