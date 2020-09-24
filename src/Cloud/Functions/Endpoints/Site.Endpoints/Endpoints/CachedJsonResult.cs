using System;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Endpoints
{
    public class CachedJsonResult : JsonResult
    {
        private int _cacheDurationInSeconds = 60;

        public CachedJsonResult(object value) : base(value)
        {
        }

        public CachedJsonResult(object value, int cacheDurationInSeconds) : base(value)
        {
            _cacheDurationInSeconds = cacheDurationInSeconds;
        }

        public CachedJsonResult(object value, JsonSerializerSettings serializerSettings) : base(value, serializerSettings)
        {
        }

        public CachedJsonResult(object value, JsonSerializerSettings serializerSettings, int cacheDurationInSeconds) : base(value, serializerSettings)
        {
            _cacheDurationInSeconds = cacheDurationInSeconds;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (Value != null)
            {
                var response = context.HttpContext.Response;
                if (_cacheDurationInSeconds > 0)
                    response.Headers.Add("cache-control", "public, max-age=" + _cacheDurationInSeconds);
                response.ContentType = "application/json";
                var data = JsonConvert.SerializeObject(Value);
                await response.WriteAsync(data);
            }
        }
    }
}