using Backgammon.ViewModels;
using System;
using Backgammon.UWP.Pages;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Backgammon.UWP.UserControlls
{
    public sealed partial class GameUC : UserControl, IUIThreadOwner
    {
        public GameViewModel viewModel { get; set; }

        public void Invoke(Action<object[]> action, params object[] parameters)
        {
            Dispatcher.RunSynchronously(action, parameters);
        }

        public GameUC()
        {
            viewModel = new GameViewModel(this);
            this.InitializeComponent();

            viewModel.GameFinished += win =>
            {
                MessageDialog msg = new MessageDialog(win ? "You Win!" : "You Lose!");
                msg.Commands.Add(new UICommand("OK", uic => (Window.Current.Content as Frame).Navigate(typeof(OpeningPage))));
                msg.CancelCommandIndex = 0;
                msg.DefaultCommandIndex = 0;

                Dispatcher.RunSynchronously(async args => await msg.ShowAsync());
            };
        }

        private void CellUC_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender == Exit)
            {
                viewModel.CellSelectedAsTarget();
                return;
            }
            var cell = sender as CellUC;
            switch (cell.CellStatus)
            {
                case CellStatus.CanBeFrom:
                    viewModel.CellSelectedAsOrgin(board.Children.IndexOf(cell) - 1);
                    return;

                case CellStatus.CanBeTarget:
                    viewModel.CellSelectedAsTarget(board.Children.IndexOf(cell) - 1);
                    return;

                case CellStatus.From: goto case CellStatus.CanBeTarget;
            }
        }
    }
}
