using GameServer.Core.Net.State;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Core.Net.Udp
{
    public class UdpPeer : UdpBase
    {
        public UdpPeer()
        {
            UdpClient.EnableBroadcast = false;
        }

        private PeerState PeerState { get; set; }

        public IPEndPoint RemoteEndPoint => UdpClient.Client.RemoteEndPoint as IPEndPoint;

        public void Connect(string hostname, int port)
        {
            UdpClient.Connect(hostname, port);

            if (!UdpClient.Client.Connected) return;

            PeerState = PeerState.Connected;

            Task.Factory.StartNew(async () =>
            {
                while (PeerState == PeerState.Connected)
                {
                    var message = await Receive();

                    MessageHandlers.OnMsg(message);
                }
            });
        }

        public void Disconnect()
        {
            PeerState = PeerState.Disconnected;
        }
    }
}
