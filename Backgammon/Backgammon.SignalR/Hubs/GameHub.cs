using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Backgammon.Common;
using Backgammon.Common.GameLogic;
using Backgammon.DAL;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace Backgammon.SignalR.Hubs
{
    [HubName("GameHub")]
    public class GameHub : Hub
    {
        public void StartGame(string askingConnctionId)
        {
            var game = new Game();
            
            ConnectionsHelper.games.Add((Context.ConnectionId, askingConnctionId), game);

            Clients.Client(askingConnctionId).StartGame(true, game.CurrentTurn, Context.ConnectionId, ConnectionsHelper.availables[Context.ConnectionId]);
            Clients.Caller.StartGame(false, game.CurrentTurn, askingConnctionId, ConnectionsHelper.availables[askingConnctionId]);
        }

        public void NotifyMovement(string opponentConnectionId, int from, int to)
        {
            (string id1, string id2) key;
            if (ConnectionsHelper.GameKeyExists(Context.ConnectionId, opponentConnectionId, out key))
            {
                var game = ConnectionsHelper.games[key];
                try
                {
                    switch (game.MakeMove(from, to))
                    {
                        case MoveResult.TurnSwitchedBack:
                            Clients.Caller.DisplayChanges2(game.CurrentTurn);
                            break;

                        case MoveResult.TurnSwitched:
                            Clients.Caller.DisplayChanges2(game.CurrentTurn);
                            Clients.Client(opponentConnectionId).DisplayChanges3(from, to, game.CurrentTurn);
                            break;

                        case MoveResult.TurnContinued:
                            Clients.Client(opponentConnectionId).DisplayChanges1(from, to);
                            break;

                        case MoveResult.GameFinished:
                            goto case MoveResult.TurnContinued;
                    }
                }
                catch (InvalidOperationException e)
                {
                    Clients.Caller.OnGameLogicError(e);
                }

            }
            else throw new InvalidOperationException();
        }
    }
}
