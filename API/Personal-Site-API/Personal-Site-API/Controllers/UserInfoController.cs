using System;
using System.Threading;
using System.Threading.Tasks;
using FluentCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Application.Entities;
using Site.Application.Users.Models;
using Site.Application.Users.Queries;

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
    }
}