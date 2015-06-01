using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly NamespaceManager _namespaceManager;
        private readonly ITopicRepository _topicRepository;

        public SubscriptionRepository(NamespaceManager namespaceManager, ITopicRepository topicRepository)
        {
            _namespaceManager = namespaceManager;
            _topicRepository = topicRepository;
        }

        public async Task<SubscriptionDescription> Get<T>(string subscriptionName)
        {
            var topic = await _topicRepository.Get<T>();

            var existsTask = _namespaceManager.SubscriptionExistsAsync(topic.Path, subscriptionName);
            var createTask = _namespaceManager.CreateSubscriptionAsync(topic.Path, subscriptionName);
            var getTask = _namespaceManager.GetSubscriptionAsync(topic.Path, subscriptionName);

            if (await existsTask)
            {
                return await getTask;
            }

            return await createTask;
        }
    }
}
