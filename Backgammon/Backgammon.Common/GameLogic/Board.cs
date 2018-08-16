using System;
using System.Collections.ObjectModel;

namespace Backgammon.Common.GameLogic
{
    public class Board
    {
        public readonly ObservableCollection<Cell> Cells;

        public PlayerColor EatenColor { get; private set; }
        public int EatenAmount { get; private set; }

        public int WhitesOut { get; private set; }
        public int BlacksOut { get; private set; }

        public Board()
        {
            Cell[] cells = new Cell[24];
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell();
                int amount = 0;
                PlayerColor color = PlayerColor.None;
                switch (i)
                {
                    case 0:
                        amount = 2;
                        color = PlayerColor.White;
                        break;
                    case 5:
                        amount = 5;
                        color = PlayerColor.Black;
                        break;
                    case 7:
                        amount = 3;
                        color = PlayerColor.Black;
                        break;
                    case 11:
                        amount = 5;
                        color = PlayerColor.White;
                        break;
                    case 12:
                        amount = 5;
                        color = PlayerColor.Black;
                        break;
                    case 16:
                        amount = 3;
                        color = PlayerColor.White;
                        break;
                    case 18:
                        amount = 5;
                        color = PlayerColor.White;
                        break;
                    case 23:
                        amount = 2;
                        color = PlayerColor.Black;
                        break;
                        //default:
                        //    goto case cells.Length - 1 - i;
                }
                cells[i].Color = color;
                cells[i].Count = amount;
            }
            Cells = new ObservableCollection<Cell>(cells);
        }

        public Cell this[int index] => Cells[index];

        public void Move(int from, int to)
        {
            if (from <= -1 || from >= 24)
            {
                var w = Cells[to];
                Cells[to] = new Cell
                {
                    Count = w.Color == EatenColor ? w.Count + 1 : 1,
                    Color = EatenColor
                };
                if (--EatenAmount <= 0) EatenColor = PlayerColor.None;
                return;
            }
            if (to > 23 || to < 0)
            {
                var taken = Cells[from];
                Cells[from] = new Cell
                {
                    Color = taken.Count - 1 != 0 ? taken.Color : PlayerColor.None,
                    Count = taken.Count - 1
                };
                if (taken.Color == PlayerColor.Black) BlacksOut++;
                else WhitesOut++;
                return;
            }
            Cell f = Cells[from], t = Cells[to];

            Cells[from] = new Cell
            {
                Color = f.Count - 1 != 0 ? f.Color : PlayerColor.None,
                Count = f.Count - 1
            };
            if (t.Color != PlayerColor.None && t.Color != f.Color)
            {
                if (EatenAmount++ == 0)
                    EatenColor = t.Color;
            }
            Cells[to] = new Cell
            {
                Color = f.Color,
                Count = t.Color == f.Color ? t.Count + 1 : 1
            };
        }
    }
}
