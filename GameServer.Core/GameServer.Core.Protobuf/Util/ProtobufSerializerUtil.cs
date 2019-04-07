using GameServer.Core.Message.Messages;
using GameServer.Core.Message.Messages.S2C;
using Google.Protobuf;
using Protobuf.S2C;
using System;
using System.Collections.Generic;

namespace GameServer.Core.Protobuf.Util
{
    public static class ProtobufSerializerUtil
    {
        private static Dictionary<MessageCode, Func<GameMessage, byte[]>> _gameMessageSerializerLookups =
            new Dictionary<MessageCode, Func<GameMessage, byte[]>>()
            {
                { MessageCode.S2CLoginResult, SerializLoginResult},
            };

        public static byte[] Serialize(GameMessage message)
        {
            if (_gameMessageSerializerLookups.ContainsKey(message.Code))
            {
                return _gameMessageSerializerLookups[message.Code](message);
            }
            return null;
        }

        private static byte[] SerializLoginResult(GameMessage message)
        {
            MessageLoginResult msg = (MessageLoginResult)message;

            LoginResult proto = new LoginResult();
            proto.LoginSuccess = msg.LoginSuccess;
            proto.SessionTicket = msg.SessionTicket;

            var data = proto.ToByteArray();
            return data;
        }
    }
}
