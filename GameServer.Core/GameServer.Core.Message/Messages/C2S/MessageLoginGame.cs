using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Core.Message.Messages.C2S
{
    public class MessageLoginGame : GameMessage
    {
        public string Email { get; set; }

        public MessageLoginGame() : base(MessageCode.C2SLoginGame)
        {
        }
    }
}
