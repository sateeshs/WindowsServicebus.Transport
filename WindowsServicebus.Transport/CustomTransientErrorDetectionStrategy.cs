using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicebus.Transport
{
    public class CustomTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {

        public bool IsTransient(Exception ex)
        {
            if (ex is FaultException)
            {
                return false;
            }
            else if (ex is CommunicationException)
            {
                return true;
            }
            else if (ex is TimeoutException)
            {
                return true;
            }
            else if (ex is WebException)
            {
                return true;
            }
            /* else if (ex is SecurityTokenException)
             {
                 return true;
             }*/
            else if (ex is ServerTooBusyException)
            {
                return true;
            }
            else if (ex is ServerErrorException)
            {
                return true;
            }
            else if (ex is InvalidOperationException)
            {
                return true;
            }
            else if (ex is EndpointNotFoundException)
            {
                // This exception may occur when a listener and a consumer are being
                // initialized out of sync (e.g. consumer is reaching to a listener that
                // is still in the process of setting up the Service Host).
                return true;
            }
            else if (ex is SocketException)
            {
                SocketException socketFault = ex as SocketException;

                return socketFault.SocketErrorCode == SocketError.TimedOut;
            }
            else if (ex is ProtocolException)
            {
                // Attempt to handle a condition upon which a client channel fails with the following exception:
                // "This channel can no longer be used to send messages as the output session was auto-closed due to a server-initiated shutdown.
                // Either disable auto-close by setting the DispatchRuntime.AutomaticInputSessionShutdown to false, or consider modifying the shutdown protocol with the remote server."
                return true;
            }

            // Some transient exceptions may be wrapped into an outer exception, hence we should also inspect any inner exceptions.
            if (ex != null && ex.InnerException != null)
            {
                return IsTransient(ex.InnerException);
            }

            return false;

        }
    }
}