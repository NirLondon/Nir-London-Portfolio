using Backgammon.Common.ChatLogic;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backgammon.ViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            Connection.Current.ChatHubProxy.On<Message>("DisplayMessage", m => MessageReceived?.Invoke(m));
        }

        public void SendMessage(string content)
        {
            Connection.Current.ChatHubProxy.Invoke("SendMessage", content, Connection.Current.Status.OpponentDetails.ConnectionId);
        }

        public event Action<Message> MessageReceived;
    }
}
