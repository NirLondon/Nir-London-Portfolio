using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;

namespace Backgammon.SignalR.Hubs
{
    public class ContactingHub : Hub
    {
        public IEnumerable<Tuple<string, string>> SendAvailables()
        {
            return ConnectionsHelper.availables
                .Where(c => c.Key != Context.ConnectionId)
                .Select(c => new Tuple<string, string>(c.Key, c.Value));
        }

        public void AskToPlay(string askedConnectionId)
        {
            Clients.Client(askedConnectionId).AnswerAsk(
                /*askingUserName:*/ ConnectionsHelper.availables[Context.ConnectionId],
                /*askingConnectionId:*/ Context.ConnectionId
                );
        }

        public void SendRefuse(string askingConnectionId)
        {
            Clients.Client(askingConnectionId).DisplayRefuse(ConnectionsHelper.availables[Context.ConnectionId]);
        }
    }
}