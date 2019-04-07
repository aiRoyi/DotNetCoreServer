using GameServer.Core.Message.Messages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GameServer.Core.Net.Message
{
    public struct UdpMessage
    {
        public IPEndPoint Sender;

        public int MessageCode;

        public GameMessage MessageData;
    }
}
