using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.GithubRepos.Models;
using Site.Application.Interfaces;

namespace Site.Infrastructure.Services
{
    public class RecurringJobService : IRecurringJobService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public RecurringJobService(IConfiguration configuration, ILogger<RecurringJobService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task UpdateGithubRepos(int userId, string username)
        {
            try
            {
                var model = new GithubMessageDto
                {
                    UserId = userId,
                    UserName = username
                };

                var QueueName = "github-repos";
                var ServiceBusConnectionString = _configuration["ServiceBusConnectionString"];
                ServiceBusConnectionString =
                    "Endpoint=sb://personal-site.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Owi7u08ajWgNs6lDp0D7v0UwzLnKqvxoNO4pPGiCLtE=";
                var queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

                // Create a new message to send to the queue.
                string messageBody = JsonConvert.SerializeObject(model);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                // Write the body of the message to the console.
                Console.WriteLine($"Sending message: {messageBody}");

                // Send the message to the queue.
                await queueClient.SendAsync(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
        }
    }
}