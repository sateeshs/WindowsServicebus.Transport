using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public interface IServiceBusManager
    {
        Task DeleteTopicAsync<T>();
        Task DeleteSubscriptionAsync<T>(string subscriptionName);
        Task CreateTopicAsync<T>();
        Task CreateSubscriptionAsync<T>(string subscriptionName);
    }
}
