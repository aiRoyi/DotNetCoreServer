using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Core.Message.Messages.S2C
{
    public class MessageLoginResult : GameMessage
    {
        public bool LoginSuccess { get; set; }

        public string SessionTicket { get; set; }

        public MessageLoginResult() : base(MessageCode.S2CLoginResult)
        {
        }
    }
}
