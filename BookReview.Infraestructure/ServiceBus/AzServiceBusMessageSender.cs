using BookReview.Application.Contracts.Infraestructure;
using BookReview.Domain.Common;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Infraestructure.ServiceBus
{
    public class AzServiceBusMessageSender : IMessageBus
    {
        private readonly IConfiguration _configuration;
        public AzServiceBusMessageSender(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public Task PublishMessage(BaseMessageBus message, string topicName)
        {
            var busString = _configuration.GetSection("Bus")["ConnectionString"];

            ISenderClient topicClient = new TopicClient(busString, topicName);
            
            var jsonMessage = JsonConvert.SerializeObject(message);

            var busMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            return topicClient.SendAsync(busMessage);
        }
    }
}
