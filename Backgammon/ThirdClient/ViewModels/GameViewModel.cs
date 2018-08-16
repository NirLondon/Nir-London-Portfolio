using Backgammon.Common.GameLogic;
using Backgammon.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdClient.ViewModels
{
    public class GameViewModel : Backgammon.ViewModels.GameViewModel, INotifyPropertyChanged
    {
        public override (int, int) Dice
        {
            get => base.Dice;
            protected set
            {
                base.Dice = value;
                Notify(nameof(Dice));
            }
        }

        public ObservableCollection<Cell> Cells { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public GameViewModel() : base()
        {
            this.BoardChanged += (from, to) => { Cells[from] = Board.Cells[from]; Cells[to] = Board[to]; };
            Cells = new ObservableCollection<Cell>(Board.Cells);
        }
    }
}
