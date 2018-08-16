using Backgammon.Common.GameLogic;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Backgammon.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        IUIThreadOwner UIThreadOwner;

        public GameViewModel(IUIThreadOwner UIThreadOwner)
        {
            this.UIThreadOwner = UIThreadOwner;
            Board = new Board();
            CellStatuses = new ObservableCollection<CellStatus>(new CellStatus[24]);

            OnGameStating += (starting, turn) =>
            {
                CurrentTurn = turn;
                Color = starting ? PlayerColor.White : PlayerColor.Black;
                if (starting) AllowToPlay();
            };

            Connection.Current.GameHubProxy.On<int, int>("DisplayChanges1", (from, to) => BoardChanged(from, to));
            Connection.Current.GameHubProxy.On<Turn>("DisplayChanges2", newTurn => BoardChanged(newTurn: newTurn));
            Connection.Current.GameHubProxy.On<int, int, Turn>("DisplayChanges3", (from, to, newTurn) => BoardChanged(from, to, newTurn));

            Connection.Current.GameHubProxy.On("StartGame", OnGameStating);

            Connection.Current.GameHubProxy.On<Exception>("OnGameLogicError", e => throw e);
        }

        public Board Board { get; }
        public ObservableCollection<CellStatus> CellStatuses { get; }
        Turn currentTurn;
        public Turn CurrentTurn
        {
            get => currentTurn;
            private set { currentTurn = value; Notify(nameof(CurrentTurn)); }
        }
        private (int from, int to)[] availableMoves;
        public (int from, int to)[] AvailableMoves
        {
            get
            {
                if (availableMoves == null)
                    availableMoves = CurrentTurn.AvailableMoves(Board).ToArray();
                return availableMoves;
            }
        }
        public PlayerColor Color { get; private set; }
        public bool IsEaten => Board.EatenColor == Color;
        bool canTakeOut;
        public bool CanTakeOut
        {
            get => canTakeOut;
            private set
            {
                canTakeOut = value;
                Notify(nameof(CanTakeOut));
            }
        }
        int indexOfFrom;
        IEnumerable<int> froms, toes;

        void AllowToPlay(bool canceled = false)
        {
            if (IsEaten)
            {
                indexOfFrom = Color == PlayerColor.White ? -1 : 24;
                foreach (int to in toes = AvailableMoves.Select(move => move.to))
                    CellStatuses[to] = CellStatus.CanBeTarget;
            }
            else
            {
                if (canceled)
                    foreach (int from in froms)
                        CellStatuses[from] = CellStatus.CanBeFrom;

                else foreach (int from in froms = AvailableMoves.Select(move => move.from).Distinct())
                        CellStatuses[from] = CellStatus.CanBeFrom;
            }
        }
        public void CellSelectedAsOrgin(int indexOfFrom)
        {
            this.indexOfFrom = indexOfFrom;

            foreach (int from in froms)
                CellStatuses[from] = CellStatus.None;

            CellStatuses[indexOfFrom] = CellStatus.From;

            toes = AvailableMoves.Where(move => move.from == indexOfFrom).Select(move => move.to);
            CanTakeOut = toes.Any(to => to > 23 || to < 0);

            foreach (var to in toes = toes.Where(move => move < 24 && move > -1))
                CellStatuses[to] = CellStatus.CanBeTarget;
        }
        public void CellSelectedAsTarget(int indexOfTarget = 24)
        {
            if (indexOfTarget == 24 && Color == PlayerColor.Black) indexOfTarget = -1;

            foreach (int to in toes) CellStatuses[to] = CellStatus.None;
            if (!IsEaten) CellStatuses[indexOfFrom] = CellStatus.None;
            CanTakeOut = false;

            if (indexOfTarget == indexOfFrom)
            {
                AllowToPlay(true);
                return;
            }

            availableMoves = null;

            switch (CurrentTurn.MakeMove(Board, indexOfFrom, indexOfTarget))
            {
                case MoveResult.TurnContinued:
                    AllowToPlay();
                    break;

                case MoveResult.TurnSwitched:
                    for (int i = 0; i < 24; i++)
                        CellStatuses[i] = CellStatus.None;
                    break;

                case MoveResult.GameFinished:
                    GameFinished.Invoke(true);
                    break;
            }

            Connection.Current.GameHubProxy.Invoke("NotifyMovement", Connection.Current.Status.OpponentDetails.ConnectionId, indexOfFrom, indexOfTarget);
        }
        public void BoardChanged(int? from = null, int? to = null, Turn newTurn = null)
        {
            if (from.HasValue && to.HasValue)
            {
                UIThreadOwner.Invoke(args =>
                {
                    switch (CurrentTurn.MakeMove(Board, from.Value, to.Value))
                    {
                        case MoveResult.TurnSwitched:
                            CurrentTurn = newTurn;
                            AllowToPlay();
                            return;

                        case MoveResult.GameFinished:
                            GameFinished?.Invoke(Board.BlacksOut == 15 && Color == PlayerColor.Black);
                            return;
                    }
                });
                return;
            }
            UIThreadOwner.Invoke(args =>
            {
                CurrentTurn = newTurn;
                if (newTurn.PlayerColor == Color)
                    AllowToPlay();
            });
        }

        public event Action<bool> GameFinished;
        public Action<bool, Turn> OnGameStating;

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public enum CellStatus : byte
    {
        None = 0,
        CanBeFrom = 1,
        CanBeTarget = 2,
        From = 3
    }
}
