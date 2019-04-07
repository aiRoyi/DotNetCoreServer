using GameServer.Core.Message.Messages;
using GameServer.Core.Message.Messages.C2S;
using Google.Protobuf;
using Protobuf;
using Protobuf.C2S;
using System;
using System.Collections.Generic;

namespace GameServer.Core.Protobuf.Util
{
    public static class ProtobufDeserializerUtil
    {
        private static Dictionary<MessageType, Func<byte[], GameMessage>> _gameInputDeserializerLookups =
            new Dictionary<MessageType, Func<byte[], GameMessage>>()
            {
                { MessageType.C2SheartBeat, DeserializeHeartBeat},
                { MessageType.C2SloginGame, DeserializeLoginGame},
            };

        public static GameMessage Deserialize(byte code, byte[] data)
        {
            try
            {
                MessageType type = (MessageType)code;
                if (_gameInputDeserializerLookups.ContainsKey(type))
                {
                    return _gameInputDeserializerLookups[type](data);
                }
            }
            catch (InvalidProtocolBufferException)
            {

            }
            return null;
        }

        private static GameMessage DeserializeHeartBeat(byte[] data)
        {
            HeartBeat heartBeat = HeartBeat.Parser.ParseFrom(data);
            if (heartBeat != null)
            {
                MessageHeartBeat msg = new MessageHeartBeat();
                return msg;
            }
            return null;
        }

        private static GameMessage DeserializeLoginGame(byte[] data)
        {
            LoginGame loginGame = LoginGame.Parser.ParseFrom(data);
            if (loginGame != null)
            {
                MessageLoginGame msg = new MessageLoginGame();
                msg.Email = loginGame.Email;
                return msg;
            }
            return null;
        }
    }
}
