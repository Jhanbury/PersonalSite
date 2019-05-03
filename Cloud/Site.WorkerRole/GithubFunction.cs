using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.GithubRepos.Models;
using Site.Application.Interfaces;
using Site.Application.Messaging;
using Site.Infrastructure.Messages;
using Willezone.Azure.WebJobs.Extensions.AzureKeyVault;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace Site.WorkerRole
{
    public static class GithubFunction
    {
        [FunctionName("GithubRepos")]
        public static async Task Run([ServiceBusTrigger("github-repos", Connection = "ConnectionString")]string myQueueItem,[Inject]IGithubService githubService, [Inject]IMessageHandlerFactory messageHandlerFactory)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, NullValueHandling = NullValueHandling.Ignore };
            var message = JsonConvert.DeserializeObject<Message>(myQueueItem, settings);
            var handler = messageHandlerFactory.ResolveMessageHandler(message);
            //await handler.ProcessAsync(message);
        }
    }
}
