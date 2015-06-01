using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly ISubscriptionClientFactory _subscription;
        public SubscriptionManager(ISubscriptionClientFactory subscription)
        {
            _subscription = subscription;
        }
        public async Task<T> Subscribe<T>(SubscriptionConfiguration subscriptionConfig)
        {
            var clientTask= _subscription.CreateFor<T>(subscriptionConfig);
            var client = await clientTask;
            var task = Task.Factory.FromAsync<BrokeredMessage>(client.BeginReceive, client.EndReceive,null);
            var bm = await task;

            return SerializationHelper.DeserializeWCFObject<T>(bm);
        }

        
    }
}
