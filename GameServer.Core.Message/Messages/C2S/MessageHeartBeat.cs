using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Core.Message.Messages.C2S
{
    public class MessageHeartBeat : GameMessage
    {
        public MessageHeartBeat() : base(MessageCode.C2SHeartBeat)
        {
        }
    }
}
