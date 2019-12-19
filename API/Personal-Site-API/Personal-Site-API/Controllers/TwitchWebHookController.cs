using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentCache;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Site.Application.Infrastructure.Models.Twitch;
using Site.Application.Interfaces;
using Site.Application.PlatformAccounts.Commands;
using Site.Application.Users.Models;
using Site.Application.Users.Queries;

namespace Personal_Site_API.Controllers
{
    [Route("api/TwitchWebhook")]
    [ApiController]
    public class TwitchWebHookController : BaseController
    {
      
      private readonly ITwitchService _twitchService;
      public TwitchWebHookController(IMediator mediator, ICache cache, ILogger logger, ITwitchService twitchService) : base(mediator, cache, logger)
      {
        
        _twitchService = twitchService;
      }

      [HttpPost]
      [Route("SteamUpdate/{userId}/{accountId}")]
      public async Task<ActionResult> GetUserInfo([FromBody]TwitchStreamUpdateResponse response, int userId, string accountId)
      {
        try
        {
          var commandData = _twitchService.HandleTwitchStreamUpdateWebhook(response, userId);
          
          var userInfo = await _mediator.Send<bool>(new UpdateAccountStreamStateCommand());
            
          return Ok(userInfo);
        }
        catch (Exception e)
        {
          _logger.LogError(e, e.Message);
          return BadRequest();
        }
      }
  }
}
