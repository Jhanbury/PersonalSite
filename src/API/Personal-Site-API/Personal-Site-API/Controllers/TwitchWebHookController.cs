using System;
using System.Threading.Tasks;
using FluentCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Application.Infrastructure.Models.Twitch;
using Site.Application.Interfaces;

namespace Personal_Site_API.Controllers
{
    [Route("api/TwitchWebhook")]
    public class TwitchWebHookController : BaseController
    {
      
      private readonly ITwitchService _twitchService;
      public TwitchWebHookController(IMediator mediator, ICache cache, ILogger<TwitchWebHookController> logger, ITwitchService twitchService) : base(mediator, cache, logger)
      {
        _twitchService = twitchService;
      }

      [HttpPost]
      [Route("StreamUpdate/{userId}/{accountId}")]
      public async Task<ActionResult> GetUserInfo([FromBody]TwitchStreamUpdateResponse response, int userId, string accountId)
      {
        try
        {
          var commandData = _twitchService.HandleTwitchStreamUpdateWebhook(response, userId, accountId);
          
          var userInfo = await _mediator.Send<bool>(commandData);
            
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
