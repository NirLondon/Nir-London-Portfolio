using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backgammon.ViewModels
{
    public class Connection
    {
        private static readonly object padlock = new object();
        private static Connection current;
        public static Connection Current
        {
            get
            {
                if (current == null)
                    lock (padlock)
                    {
                        if (current == null)
                            current = new Connection();
                    }
                return current;
            }
        }

        readonly HubConnection hubConnection;
        public readonly IHubProxy AuthenticationHubProxy, ContactingHubProxy, GameHubProxy, ChatHubProxy;

        public readonly PlayingStatusSettings Status;
        private Connection()
        {
            Status = new PlayingStatusSettings();

            hubConnection = new HubConnection("http://localhost:61711/");
            AuthenticationHubProxy = hubConnection.CreateHubProxy("AuthenticationHub");
            ContactingHubProxy = hubConnection.CreateHubProxy("ContactingHub");
            GameHubProxy = hubConnection.CreateHubProxy("GameHub");
            ChatHubProxy = hubConnection.CreateHubProxy("ChatHub");
            hubConnection.Start().Wait();
        }

        public async void LogOut()
        {
            await AuthenticationHubProxy.Invoke("LogOut", Status.UserName);
            lock (padlock)
            {
                hubConnection.Stop();
                current = null;
            }
        }

        public class PlayingStatusSettings
        {
            public string UserName { get; set; }
            public (string UserName, string ConnectionId) OpponentDetails { get; set; }

            public bool IsPlaying => OpponentDetails != (null, null);
            public bool IsConnected => UserName != null;
        }
    }
}