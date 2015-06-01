using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public class TopicClientFactory : ITopicClientFactory
    {
        private readonly MessagingFactory _messagingFactory;
        private readonly ITopicRepository _topicRepository;

        public TopicClientFactory(MessagingFactory messagingFactory, ITopicRepository topicRepository)
        {
            _messagingFactory = messagingFactory;
            _topicRepository = topicRepository;
        }

        public async Task<TopicClient> CreateFor<T>()
        {
            var topic = await _topicRepository.Get<T>();
            return _messagingFactory.CreateTopicClient(topic.Path);

            //.ContinueWith(x => _messagingFactory.CreateTopicClient(x.Result.Path));
        }
    }
}
