using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public class SubscriptionClientFactory : ISubscriptionClientFactory
    {
        private readonly MessagingFactory _messagingFactory;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionClientFactory(MessagingFactory messagingFactory,
            ISubscriptionRepository subscriptionRepository)
        {
            _messagingFactory = messagingFactory;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<SubscriptionClient> CreateFor<T>(SubscriptionConfiguration config)
        {
            var subscription = await _subscriptionRepository.Get<T>(config.SubscriptionName);
            var topicPath = subscription.TopicPath;
            //TODO async
            return _messagingFactory.CreateSubscriptionClient(topicPath, subscription.Name, config.ReceiveMode);
        }
    }
}
