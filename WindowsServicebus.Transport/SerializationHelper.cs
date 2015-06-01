using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsServicebus.Transport
{
    internal static class SerializationHelper
    {
       /* private readonly SubscriptionConfiguration _subscriptionConfig;
        internal SerializationHelper(SubscriptionConfiguration subscriptionConfig)
        {
            _subscriptionConfig = subscriptionConfig;
        }
        internal static GetMessage<T>(T inboundMessage){
        MessgeSerialization payloadType=  _subscriptionConfig.SerializationType;
    }*/
        internal static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                DefaultMembersSearchFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance
            }
        };

        internal static string SerializeObject(object target)
        {
            return JsonConvert.SerializeObject(target);
        }

        internal static T DeserializeObject<T>(string target)
            where T : class
        {
            return JsonConvert.DeserializeObject<T>(target, Settings);
        }

        internal static StringBuilder SerializeWCFObject<T>(T outboundMessage)
        {
            var sb = new StringBuilder();
            var xmlWrite = new XmlTextWriter(new StringWriter(sb));
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(xmlWrite, outboundMessage);
            return sb;

        }

        internal static T DeserializeWCFObject<T>(BrokeredMessage inboundMessage)
            //where T : class
        {
            return inboundMessage.GetBody<T>(new DataContractSerializer(typeof(T)));
        }

    }
}
