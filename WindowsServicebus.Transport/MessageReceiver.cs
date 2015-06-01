using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public class MessageReceiver : IMessageReceiver
    {
        ISubscriptionClientFactory _subscription;
        public MessageReceiver(ISubscriptionClientFactory subscription)
        {
            _subscription = subscription;
        }
        public Task<T> Receive<T>(SubscriptionConfiguration config, IObserver<T> observer)
        {
            throw new NotImplementedException();
        }
    }
}
