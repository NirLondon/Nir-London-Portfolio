using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Backgammon.DAL;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Backgammon.SignalR.Hubs
{
    [HubName("AuthenticationHub")]
    public class AuthenticationHub : Hub
    {
        DBManager db = new DBManager();

        public bool Login(string userName, string password)
        {
            if (db.GetUser(userName, password) != null)
            {
                if (!ConnectionsHelper.availables.ContainsKey(Context.ConnectionId))
                    ConnectionsHelper.availables.Add(Context.ConnectionId, userName);
                return true;
            }
            return false;
        }

        public bool Signin(string userName, string password)
        {
            if (db.AddUser(userName, password))
            {
                Login(userName, password);
                return true;
            }
            return false;
        }

        public bool LogOut(string userName)
        {
            ConnectionsHelper.availables.Remove(Context.ConnectionId);

            (string id1, string id2) key = (null, null);
            if (ConnectionsHelper.games.Keys.Any(k =>
             {
                 if (k.id1 == Context.ConnectionId || k.id2 == Context.ConnectionId)
                 {
                     key = k;
                     return true;
                 }
                 return false;
             }))
            {
                ConnectionsHelper.games.Remove(key);
                Clients.Client(key.id1 == Context.ConnectionId? key.id2 : key.id1).OnOpponentLogedOut();
            }


                return true;
        }
    }
}