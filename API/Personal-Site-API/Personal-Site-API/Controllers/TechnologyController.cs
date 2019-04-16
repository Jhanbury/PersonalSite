using System.Collections.Generic;
using System.Threading.Tasks;
using FluentCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Site.Application.Technologies.Models;
using Site.Application.Technologies.Queries.GetAllTechnologies;

namespace Personal_Site_API.Controllers
{
    [Route("api/technologies")]
    public class TechnologyController : BaseController
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<TechnologyDto>>> GetAll()
        {
            var list = await _mediator.Send<List<TechnologyDto>>(new GetAllTechnologiesQuery());
            return Ok(list);
        }

        public TechnologyController(IMediator mediator, ICache cache, ILogger<TechnologyController> logger) : base(mediator, cache, logger)
        {
        }
    }
}