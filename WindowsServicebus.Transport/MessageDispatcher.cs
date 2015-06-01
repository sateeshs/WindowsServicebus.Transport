using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly ITopicClientFactory _topicClientFactory;
        public MessageDispatcher(ITopicClientFactory topicClientFactory)
        {
            _topicClientFactory = topicClientFactory;
        }

        public async Task PublishAsync<T>(T message)
        {
            var topicClient = await _topicClientFactory.CreateFor<T>();
            try
            {
                var task = Task.Factory.FromAsync(topicClient.BeginSend, topicClient.EndSend, new BrokeredMessage(message), null);
                await task; 
            }
            finally
            {
                topicClient.Close();
            }

        }

    }
}
