using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public interface IMessageReceiver
    {
        Task<T> Receive<T>(SubscriptionConfiguration config, IObserver<T> observer);
    }
}
