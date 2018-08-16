using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.Common.GameLogic
{
    public struct Cell
    {
        public PlayerColor Color { get; internal set; }
        public int Count { get; internal set; }

        public static Cell operator ++(Cell cell) => new Cell { Color = cell.Color, Count = cell.Count + 1 };
        public static Cell operator --(Cell cell) => new Cell { Color = cell.Color, Count = cell.Count - 1 };
    }
}