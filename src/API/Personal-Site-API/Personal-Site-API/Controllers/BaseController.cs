using FluentCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Personal_Site_API.Controllers
{
    [ApiController]
    
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly Cache<IMediator> _cache;
        protected readonly ILogger _logger;

        public BaseController(IMediator mediator, ICache cache, ILogger logger)
        {
            _mediator = mediator;
            _cache = cache.WithSource(mediator);
            _logger = logger;
        }
    }
}