using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public class TopicRepository : ITopicRepository
    {
        private readonly NamespaceManager _namespaceManager;

        public TopicRepository(NamespaceManager namespaceManager)
        {
            _namespaceManager = namespaceManager;
        }

        public async Task<Microsoft.ServiceBus.Messaging.TopicDescription> Get<T>()
        {
            var topicName = string.Format("Topic_{0}", typeof(T).Name);

            var existsTask = _namespaceManager.TopicExistsAsync(topicName);
            var createTask = _namespaceManager.CreateTopicAsync(topicName);
            var getTask = _namespaceManager.GetTopicAsync(topicName);

            if (await existsTask)
            {
                return await getTask;
            }
            return await createTask;
        }
    }
}
