using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.Messaging;
using Site.Infrastructure.Messages;

namespace Site.Worker
{
    public class RunJob
    {
        private readonly IMessageHandlerFactory _messageHandlerFactory;

        public RunJob(IMessageHandlerFactory messageHandlerFactory)
        {
            _messageHandlerFactory = messageHandlerFactory;
        }

        [FunctionName("RunJob")]
        public async Task Run([ServiceBusTrigger("jobs", Connection = "ServiceBusConnectionString")]string myQueueItem, ILogger log)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, NullValueHandling = NullValueHandling.Ignore };
            var message = JsonConvert.DeserializeObject<Message>(myQueueItem, settings);
            var handler = _messageHandlerFactory.ResolveMessageHandler(message);
            await handler.ProcessAsync(message);
        }
    }
}
