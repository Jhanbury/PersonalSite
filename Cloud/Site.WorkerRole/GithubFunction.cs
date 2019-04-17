using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.GithubRepos.Models;
using Site.Application.Interfaces;
using Willezone.Azure.WebJobs.Extensions.AzureKeyVault;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace Site.WorkerRole
{
    public static class GithubFunction
    {
        [FunctionName("GithubRepos")]
        public static async Task Run([ServiceBusTrigger("github-repos", Connection = "ConnectionString")]string myQueueItem,[Inject]IGithubService githubService, ILogger log)
        {
            var entity = JsonConvert.DeserializeObject<GithubMessageDto>(myQueueItem);
            log.LogInformation($"Starting Github Job, Timestamp:{DateTime.Now}, UserId: {entity.UserId}, Github UserName: {entity.UserName}");
            await githubService.UpdateGithubReposForUser(entity.UserId, entity.UserName);
        }
    }
}
