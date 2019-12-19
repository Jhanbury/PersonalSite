using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.BlogPosts.Models;
using Site.Application.GithubRepos.Models;
using Site.Application.Interfaces;
using Site.Infrastructure.Messages;
using Message = Microsoft.Azure.ServiceBus.Message;

namespace Site.Infrastructure.Services
{
    public class RecurringJobService : IRecurringJobService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private const string _queueName = "jobs";
        public RecurringJobService(IConfiguration configuration, ILogger<RecurringJobService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task UpdateGithubRepos(int userId, string username)
        {
            try
            {
                var model = new GithubMessage
                {
                    UserId = userId,
                    UserName = username
                };
                
                await AddJobToQueue(_queueName, model);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
        }

        private async Task AddJobToQueue(string queue, object data)
        {
            var ServiceBusConnectionString = _configuration["ServiceBusConnectionString"];
            var queueClient = new QueueClient(ServiceBusConnectionString, queue);

            // Create a new message to send to the queue.
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, NullValueHandling = NullValueHandling.Ignore };
            string messageBody = JsonConvert.SerializeObject(data,settings);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            // Write the body of the message to the console.
            Console.WriteLine($"Sending message: {messageBody}");

            // Send the message to the queue.
            await queueClient.SendAsync(message);
        }

        public async Task UpdateUserBlogs(int userId)
        {
            try
            {
                var model = new BlogPostMessage
                {
                    UserId = userId
                };
                await AddJobToQueue(_queueName, model);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        public async Task UpdateVideoPlatforms(int userId)
        {
            try
            {
                var model = new VideoPlatformsMessage(userId);
                await AddJobToQueue(_queueName, model);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        public async Task SubscribeToTwitchWebhooks(int userId)
        {
            var message = new TwitchWebhookSubscriptionMessage();
            message.UserId = userId;
            await AddJobToQueue(_queueName, message);
        }
    }
}
