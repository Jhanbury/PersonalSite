using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.Interfaces.Messaging;
using Site.Infrastructure.Messages;

namespace Site.Worker
{
    public class Worker
    {
        private readonly IMessageHandlerFactory _messageHandlerFactory;

        public Worker(IMessageHandlerFactory messageHandlerFactory)
        {
            _messageHandlerFactory = messageHandlerFactory;
        }

        [FunctionName("Worker")]
        public async Task HandleJob([ServiceBusTrigger("jobs", Connection = "ServiceBusConnectionString")]string myQueueItem, ILogger log)
        {
            try
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, NullValueHandling = NullValueHandling.Ignore };
                var message = JsonConvert.DeserializeObject<Message>(myQueueItem, settings);
                var handler = _messageHandlerFactory.ResolveMessageHandler(message);
                await handler.ProcessAsync(message);
            }
            catch (Exception e)
            {
                log.LogError(e,e.Message);
            }
            
        }
    }
}
