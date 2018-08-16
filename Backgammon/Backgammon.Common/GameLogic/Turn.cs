using System;
using System.Collections.Generic;
using System.Linq;

namespace Backgammon.Common.GameLogic
{
    public class Turn
    {
        public int Dice1 { get; private set; }
        public int Dice2 { get; private set; }
        public bool Dice1Played { get; private set; }
        public bool Dice2Played { get; private set; }
        public readonly bool isDouble;

        int MovesLeft;
        public readonly PlayerColor PlayerColor;

        public Turn(int dice1, int dice2, PlayerColor playerColor)
        {
            Dice1 = dice1;
            Dice2 = dice2;
            PlayerColor = playerColor;
            isDouble = dice1 == dice2;
            MovesLeft = isDouble ? 4 : 2;
        }

        public IEnumerable<(int from, int to)> AvailableMoves(Board board)
        {
            bool isWhite = PlayerColor == PlayerColor.White;
            int ind1, ind2;
            if (board.EatenColor == PlayerColor)
            {
                ind1 = isWhite ? Dice1 - 1 : 23 - Dice1 + 1;
                ind2 = isWhite ? Dice2 - 1 : 23 - Dice2 + 1;

                if (!Dice1Played && board[ind1].Count <= 1 || board[ind1].Color == PlayerColor) yield return ((isWhite ? -1 : 24, ind1));
                if (!Dice2Played && board[ind2].Count <= 1 || board[ind2].Color == PlayerColor) yield return ((isWhite ? -1 : 24, ind2));
            }
            else
                for (int i = 0; i < 24; i++)
                {
                    if (board[i].Color == PlayerColor)
                    {
                        ind1 = i + (isWhite ? Dice1 : -Dice1);
                        ind2 = i + (isWhite ? Dice2 : -Dice2);

                        if (!Dice1Played &&
                            ((isWhite ? ind1 > 23 : ind1 < 0) || board[ind1].Count <= 1 || board[ind1].Color == PlayerColor))
                            yield return ((i, ind1));

                        if ((Dice1Played || !isDouble) && !Dice2Played &&
                            ((isWhite ? ind2 > 23 : ind2 < 0) || board[ind2].Count <= 1 || board[ind2].Color == PlayerColor))
                            yield return ((i, ind2));
                    }
                }
        }

        public MoveResult MakeMove(Board board, int from, int to)
        {
            if (MovesLeft <= 0)
                throw new InvalidOperationException("A player can not make moves when it is not his turn.");
            if (from < 0) from = -1;
            if (from > 23) from = 24;
            int steps = PlayerColor == PlayerColor.White ? to - from : from - to;

            if (steps < 0) throw new InvalidOperationException("Can not go backwards.");

            if (to > 23 || to < 0)
            {
                bool isWhite = PlayerColor == PlayerColor.White;
                if (isWhite ? from + Dice1 > 23 : from - Dice1 < 0 != (isWhite ? from + Dice2 > 23 : from - Dice2 < 0))
                    if (isWhite ? from + Dice1 > 23 : from - Dice1 < 0) Dice1Played = true;
                    else Dice2Played = true;
            }
            else if (isDouble && steps == Dice1)
                switch (MovesLeft)
                {
                    case 2:
                        Dice1Played = true;
                        break;
                    case 1:
                        Dice2Played = true;
                        break;
                }
            else if (!Dice1Played && steps == Dice1) Dice1Played = true;
            else if (!Dice2Played && steps == Dice2) Dice2Played = true;
            else throw new InvalidOperationException("The amount of steps in a move must be the result of one of the dice.");

            board.Move(from, to);
            MovesLeft--;

            if (board.BlacksOut == 15 || board.WhitesOut == 15)
                return MoveResult.GameFinished;

            if (!CanMove(board))
                return MoveResult.TurnSwitched;

            return MoveResult.TurnContinued;
        }

        public bool CanMove(Board board)
        {
            if (MovesLeft <= 0) return false;
            if (board.EatenColor == PlayerColor)
            {
                if (!Dice1Played && board.Cells[Dice1 - 1].Count <= 1) return true;
                if (!Dice2Played && board.Cells[Dice2 - 1].Count <= 1) return true;
                return false;
            }

            int i = 0;
            return board.Cells
                .Select(c => new { index = i++, cell = c })
                .Where(c => c.cell.Color == PlayerColor)
                .Select(c => c.index)
                .Any(from =>
                {
                    int to1 = from, to2 = from;
                    if (PlayerColor == PlayerColor.White)
                    {
                        to1 += Dice1;
                        to2 += Dice2;
                    }
                    else
                    {
                        to1 -= Dice1;
                        to2 -= Dice2;
                    }
                    return (!Dice1Played && (to1 > 23 || to1 < 0 || board[to1].Count <= 1 || board[to1].Color == PlayerColor)) ||
                    (!Dice2Played && (to2 > 23 || to2 < 0 || board[to2].Count <= 1 || board[to2].Color == PlayerColor));
                });
        }
    }
}
