using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Domain.Entities.Audit;

namespace Site.Application.Infrastructure
{
  public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly IHttpContextAccessor _httpAccessor;

    public RequestPerformanceBehaviour(ILogger<TRequest> logger, IHttpContextAccessor accessor)
    {
      _httpAccessor = accessor;
      _timer = new Stopwatch();

      _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      var startTime = DateTime.Now;
      _timer.Start();

      var response = await next();

      _timer.Stop();
      var endTime = DateTime.Now;

      var performanceLog = new PerformanceLog
      {
        StartTime = startTime,
        EndTime = endTime,
        RequestData = JsonConvert.SerializeObject(request),
        RequestName = request.GetType().Name,
        ClientIPAddress = _httpAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "N/A"
      };


      return response;
    }
  }
}
