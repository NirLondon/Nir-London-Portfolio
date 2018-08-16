using Backgammon.Common.GameLogic;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.ViewModels
{
    public class ContactingViewModel
    {
        public ObservableCollection<Tuple<string, string>> Availables { get; private set; }

        public ContactingViewModel()
        {
            var avails = Connection.Current.ContactingHubProxy
                .Invoke<IEnumerable<Tuple<string, string>>>("SendAvailables").Result;

            Availables = new ObservableCollection<Tuple<string, string>>(avails);

            Connection.Current.GameHubProxy.On<bool, Turn, string, string>("StartGame", OnGameStarting);
            Connection.Current.ContactingHubProxy.On<string, string>("AnswerAsk", AnswerToAsk);
            Connection.Current.ContactingHubProxy.On<string>("DispalyRefuse", OnRequestRefused);
        }

        void OnGameStarting(bool starting, Turn turn, string opponentConnectionId, string opponentUserName)
        {
            Connection.Current.Status.OpponentDetails = (opponentUserName, opponentConnectionId);
            GameStarting?.Invoke(starting, turn);
        }
        public event Action<bool, Turn> GameStarting;

        public void AskToPlay(string opponentConnectionId)
        {
            Connection.Current.ContactingHubProxy.Invoke("AskToPlay", opponentConnectionId);
        }

        public event Func<string, bool> AskedToPlay;
        void AnswerToAsk(string askingUserName, string askingConnectionId)
        {
            if (AskedToPlay.Invoke(askingUserName))
                Connection.Current.GameHubProxy.Invoke("StartGame", askingConnectionId);
            else Connection.Current.ContactingHubProxy.Invoke("SendRefuse", askingConnectionId);
        }

        public event Action<string> RequestRefused;
        void OnRequestRefused(string refuser)
        {
            RequestRefused?.Invoke(refuser);
        }
    }
}
