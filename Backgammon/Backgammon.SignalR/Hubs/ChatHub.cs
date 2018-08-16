using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Backgammon.Common.ChatLogic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Backgammon.SignalR.Hubs
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public void SendMessage(string messageContemt, string opponentConnectionId)
        {
            var msg = new Message
            {
                Content = messageContemt,
                IsMine = true,
                SendingTime = DateTime.Now,
                SenderName = ConnectionsHelper.availables[Context.ConnectionId]
            };

            Clients.Caller.DisplayMessage(msg);

            msg.IsMine = false;
            Clients.Client(opponentConnectionId).DisplayMessage(msg);
        }
    }
}