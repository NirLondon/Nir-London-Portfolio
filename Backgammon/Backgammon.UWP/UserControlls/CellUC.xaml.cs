using Backgammon.Common.GameLogic;
using Backgammon.ViewModels;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Backgammon.UWP.UserControlls
{
    public sealed partial class CellUC : UserControl
    {
        public CellUC()
        {
            this.InitializeComponent();
        }
        public Cell Cell
        {
            set
            {
                spEllipses.Children.Clear();
                for (int i = 0; i <value.Count ; i++)
                {
                    spEllipses.Children.Add(new Ellipse
                    {
                        Width = 30,
                        Height = 30,
                        Fill = value.Color == PlayerColor.White ? yellow : brown
                    });
                }
            }
        }

        static SolidColorBrush yellow = new SolidColorBrush(Colors.Yellow);
        static SolidColorBrush brown = new SolidColorBrush(Colors.Brown);

        static SolidColorBrush trasparent= new SolidColorBrush(Colors.Transparent);
        static SolidColorBrush blue = new SolidColorBrush(Color.FromArgb(128, 173, 216, 230));
        static SolidColorBrush red = new SolidColorBrush(Color.FromArgb(75, 250, 128, 114));

        private CellStatus cellStatus;
        public CellStatus CellStatus
        {
            get => cellStatus;
            set
            {
                cellStatus = value;

                Brush fill;
                switch (value)
                {
                    case CellStatus.From:
                        fill = red;
                        break;
                    case CellStatus.CanBeTarget:
                        fill = blue;
                        break;
                    default:
                        fill = trasparent;
                        break;
                }
                spEllipses.Background = fill;
            }
        }
    }
}
