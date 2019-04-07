using GameServer.Core.Message.Messages;
using GameServer.Core.Net.Message;
using GameServer.Core.Protobuf.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Core.Net.Udp
{
    public abstract class UdpBase
    {
        public readonly MessageHandlers MessageHandlers;

        protected UdpClient UdpClient;

        protected UdpBase()
        {
            UdpClient = new UdpClient();

            MessageHandlers = new MessageHandlers(this);
        }

        public void Send<T>(T message, IPEndPoint remoteEndPoint)
            where T : GameMessage
        {
            var data = ProtobufSerializerUtil.Serialize(message);

            var datagram = new byte[data.Length + 4];

            byte[] codeBytes;

            codeBytes = BitConverter.GetBytes((int)message.Code);

            codeBytes.CopyTo(datagram, 0);

            data.CopyTo(datagram, 4);

            UdpClient.Send(datagram, datagram.Length, remoteEndPoint);
        }

        public void Send<T>(T message)
            where T : GameMessage
        {
            var data = ProtobufSerializerUtil.Serialize(message);

            var datagram = new byte[data.Length + 4];

            byte[] codeBytes;

            codeBytes = BitConverter.GetBytes((int)message.Code);

            codeBytes.CopyTo(datagram, 0);

            data.CopyTo(datagram, 4);

            if (UdpClient.Client.Connected)
                UdpClient.Send(datagram, datagram.Length);
        }

        protected async Task<UdpMessage> Receive()
        {
            var result = await UdpClient.ReceiveAsync();

            var code = BitConverter.ToInt32(result.Buffer, 0);

            var data = new byte[result.Buffer.Length - 4];
            Array.Copy(result.Buffer, 4, data, 0, result.Buffer.Length - 4);

            var message = ProtobufDeserializerUtil.Deserialize((byte)code, data);
            return new UdpMessage
            {
                Sender = result.RemoteEndPoint,
                MessageCode = code,
                MessageData = message
            };
        }
    }
}
