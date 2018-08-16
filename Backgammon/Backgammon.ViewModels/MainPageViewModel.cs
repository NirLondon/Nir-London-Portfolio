using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backgammon.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            Connection.Current.AuthenticationHubProxy.On("OnOpponentLogedOut", OnOpponentLogedOut);
        }

        void OnOpponentLogedOut()
        {
            string opName = Connection.Current.Status.OpponentDetails.UserName;
            Connection.Current.Status.OpponentDetails = (null, null);
            OpponentLogedOut?.Invoke(opName);
        }
        public event Action<string> OpponentLogedOut;
    }
}
