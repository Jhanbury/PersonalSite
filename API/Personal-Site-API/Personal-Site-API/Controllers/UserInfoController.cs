using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.BlogPosts.Models;
using Site.Application.BlogPosts.Queries.GetUserBlogPosts;
using Site.Application.Entities;
using Site.Application.Hobbies.Model;
using Site.Application.Hobbies.Querys;
using Site.Application.Interfaces;
using Site.Application.Messaging;
using Site.Application.SocialMediaAccounts.Models;
using Site.Application.SocialMediaAccounts.Queries;
using Site.Application.Users.Models;
using Site.Application.Users.Queries;
using Site.Infrastructure.Messages;

namespace Personal_Site_API.Controllers
{
    [Route("api/userinfo")]
    public class UserInfoController : BaseController
    {
        private readonly IMessageHandlerFactory _messageHandlerFactory;
        public UserInfoController(IMediator mediator, ICache cache, ILogger<UserInfoController> logger, IMessageHandlerFactory factory) : base(mediator, cache, logger)
        {
            _messageHandlerFactory = factory;
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
        [Route("TestFactory")]
        public async void TestFactory()
        {
            IMessage dto = new GithubMessage() {UserId = 1, UserName = "test"};
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, NullValueHandling = NullValueHandling.Ignore };
            var myQueueItem = JsonConvert.SerializeObject(dto,settings);
            var message = JsonConvert.DeserializeObject<Message>(myQueueItem, settings);
            var handler = _messageHandlerFactory.ResolveMessageHandler(message);
        }
    }
}