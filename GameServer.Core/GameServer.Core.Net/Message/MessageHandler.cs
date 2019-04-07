using GameServer.Core.Message.Messages;
using GameServer.Core.Net.Udp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Core.Net.Message
{
    public abstract class MessageHandler
    {
        private UdpBase _udpBase;

        protected IPEndPoint MsgEndPoint;

        protected GameMessage Message;

        public void SetUdpBase(UdpBase udpBase)
        {
            _udpBase = udpBase;
        }

        protected void Send<T>(T message) where T : GameMessage
        {
            _udpBase.Send(message, MsgEndPoint);
        }

        protected void Send<T>(T message, IPEndPoint endPoint) where T : GameMessage
        {
            _udpBase.Send(message, endPoint);
        }

        public void StartExecute(IPEndPoint endPoint, GameMessage message)
        {
            MsgEndPoint = endPoint;
            Message = message;
            Execute();
        }

        protected abstract void Execute();
    }

    public abstract class MessageHandler<TMessage> : MessageHandler
        where TMessage : GameMessage
    {
        protected override void Execute()
        {
            try
            {
                ProcessMessageAsync(Message as TMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected abstract Task ProcessMessageAsync(TMessage message);
    }

    public class MessageHandlers
    {
        private readonly Dictionary<int, MessageHandler> _mHandlers
            = new Dictionary<int, MessageHandler>();

        private readonly UdpBase _mUdpBase;

        public MessageHandlers(UdpBase udpBase)
        {
            _mUdpBase = udpBase;
        }

        public void Add<T>(int code)
            where T : MessageHandler, new()
        {
            var handler = new T();

            handler.SetUdpBase(_mUdpBase);

            _mHandlers.Add(code, handler);
        }

        public void OnMsg(UdpMessage msg)
        {
            var code = msg.MessageCode;

            if (_mHandlers.ContainsKey(code))
                _mHandlers[code].StartExecute(msg.Sender, msg.MessageData);
        }
    }
}
