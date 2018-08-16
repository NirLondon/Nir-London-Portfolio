using Backgammon.Common.GameLogic;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backgammon.SignalR.Hubs
{
    public class ConnectionsHelper
    {
        //key: connectionId, value: userName
        public static readonly Dictionary<string, string> availables = new Dictionary<string, string>();

        public static Dictionary<(string id1, string id2), Game> games =
               new Dictionary<(string id1, string id2), Game>();

        public static bool GameKeyExists(string id1, string id2, out (string id1, string id2) key)
        {
            var tempKey = default((string, string));
            bool result = games.Keys.Any(k =>
                {
                    if (k.id1 == id1 && k.id2 == id2)
                    {
                        tempKey = (id1, id2);
                        return true;
                    }
                    if (k.id1 == id2 && k.id2 == id1)
                    {
                        tempKey = (id2, id1);
                        return true;
                    }
                    return false;
                });
            key = tempKey;
            return result;
        }
    }
}