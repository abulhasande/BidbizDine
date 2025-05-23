using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dine.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private string conncectionString = "";
        public async Task PublishMessage(object message, string topic_queue_Name)
        {
            await using var client = new ServiceBusClient(conncectionString);

            ServiceBusSender sender = client.CreateSender(topic_queue_Name);
            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalmessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await sender.SendMessageAsync(finalmessage);
            await client.DisposeAsync();
        }
    }
}
