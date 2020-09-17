using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Site.Application.Interfaces;

namespace Scheduler
{
    public class JobScheduler
    {
        private readonly IRecurringJobService _recurringJobService;
        private readonly int _userId;
        private readonly string _username;
        public JobScheduler(IRecurringJobService recurringJobService, IConfiguration config)
        {
            _recurringJobService = recurringJobService;
            _userId = config.GetValue<int>("User:Id");
            _username = config.GetValue<string>("User:Github:Id");
        }

        [FunctionName("DailyJobs")]
        public async Task DailyJobs([TimerTrigger("* * * * *")] TimerInfo myTimer, ILogger log)
        {
            
            await _recurringJobService.UpdateGithubRepos(_userId, _username);
            log.LogInformation("Update Github Repos Message Posted");
            await _recurringJobService.UpdateUserBlogs(_userId);
            log.LogInformation("Update Blogs Message Posted");
            await _recurringJobService.UpdateVideoPlatforms(_userId);
            log.LogInformation("Update Videos Message Posted");
        }

        [FunctionName("Every10Days")]
        public async Task TenDayScheduler([TimerTrigger("* * * * *")] TimerInfo myTimer, ILogger log)
        {

            await _recurringJobService.SubscribeToTwitchWebhooks(_userId);
            log.LogInformation("Update Twitch WebHook Subscription Posted");
            }
    }
}
