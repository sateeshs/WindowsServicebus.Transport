using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public interface ISubscriptionRepository
    {
        Task<SubscriptionDescription> Get<T>(string subscriptionName);
    }
}
