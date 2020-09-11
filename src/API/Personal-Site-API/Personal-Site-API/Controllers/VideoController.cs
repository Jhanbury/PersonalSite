using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentCache;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Application.Videos.Models;
using Site.Application.Videos.Queries;


namespace Personal_Site_API.Controllers
{
    [Route("api/videos")]
    [ApiController]
    public class VideoController : BaseController
    {
        public VideoController(IMediator mediator, ICache cache, ILogger logger) : base(mediator, cache, logger)
        {
        }

        
    }
}