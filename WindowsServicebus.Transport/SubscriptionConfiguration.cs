using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public class SubscriptionConfiguration
    {
        public SubscriptionConfiguration(string name)
        {
            SubscriptionName = name;
            ConfigAction = c => { };
        }

        public ReceiveMode ReceiveMode { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public string SubscriptionName { get; private set; }

        public MessgeSerialization SerializationType { get; set; }
        /// <summary>
        /// Configure the client
        /// </summary>
        /// <remarks>Temporary full access to the client</remarks>
        public Action<SubscriptionClient> ConfigAction { get; set; }

    }
    public enum MessgeSerialization
    {
        WCF,JSON,Stream
    }
}
