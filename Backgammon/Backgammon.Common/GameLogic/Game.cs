using System;

namespace Backgammon.Common.GameLogic
{
    public class Game
    {
        public readonly Board board = new Board();

        public Turn CurrentTurn { get; private set; }

        public Game()
        {
            Random r = new Random();
            CurrentTurn = new Turn(r.Next(1, 7), r.Next(1, 7), PlayerColor.White);
        }

        public MoveResult MakeMove(int from, int to)
        {
            CurrentTurn.MakeMove(board, from, to);

            if (board.BlacksOut == 15 || board.WhitesOut == 15)
                return MoveResult.GameFinished;

            if (!CurrentTurn.CanMove(board))
            {
                SwitchTurn();
                if (!CurrentTurn.CanMove(board))
                {
                    do SwitchTurn(); while (!CurrentTurn.CanMove(board));
                    return MoveResult.TurnSwitchedBack;
                }
                return MoveResult.TurnSwitched;
            }
            return MoveResult.TurnContinued;
        }

        void SwitchTurn()
        {
            Random r = new Random();
            CurrentTurn = new Turn(r.Next(1, 7), r.Next(1, 7),
                CurrentTurn.PlayerColor == PlayerColor.Black ? PlayerColor.White : PlayerColor.Black);
        }
    }

    public enum MoveResult : byte
    {
        TurnContinued = 0,
        TurnSwitched = 1,
        TurnSwitchedBack = 2,
        GameFinished = 3
    }
}
