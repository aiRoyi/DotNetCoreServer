using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Core.Message.Messages
{
    [Serializable]
    public enum MessageCode : byte
    {
        C2SHeartBeat = 0,
        C2SLoginGame = 1,
        S2CLoginResult = 2,
    }

    public abstract class GameMessage
    {
        public MessageCode Code { get; }

        public GameMessage(MessageCode code)
        {
            this.Code = code;
        }
    }
}
