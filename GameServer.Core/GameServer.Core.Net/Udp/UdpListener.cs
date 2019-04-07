using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Core.Net.Udp
{
    public class UdpListener : UdpBase
    {
        public UdpListener(IPEndPoint ipEndPoint)
        {
            ListenEndPoint = ipEndPoint;

            UdpClient = new UdpClient(ipEndPoint);
        }

        private bool IsListening { get; set; }

        public IPEndPoint ListenEndPoint { get; }

        public void Start()
        {
            IsListening = true;

            Task.Factory.StartNew(async () =>
            {
                while (IsListening)
                {
                    var message = await Receive();

                    MessageHandlers.OnMsg(message);
                }
            });
        }

        public void Stop()
        {
            IsListening = false;
        }
    }
}
