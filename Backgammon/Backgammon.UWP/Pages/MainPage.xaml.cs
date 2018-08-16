using Backgammon.Common.GameLogic;
using Backgammon.ViewModels;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Backgammon.UWP.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel { get; set; }
        public MainPage()
        {
            ViewModel = new MainPageViewModel();
            ViewModel.OpponentLogedOut += OpponentUserName =>
            {
                var msg = new MessageDialog($"{OpponentUserName} just loged out.");
                msg.Commands.Add(new UICommand("OK", uic => Dispatcher.RunSynchronously(a => Frame.Navigate(typeof(OpeningPage)))));
                Dispatcher.RunSynchronously(async agrs => await msg.ShowAsync());
            };
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is ValueTuple<bool, Turn> parameters)
                gameUc.viewModel.OnGameStating.Invoke(parameters.Item1, parameters.Item2);

            base.OnNavigatedTo(e);
        }

        void LogOut()
        {
            Connection.Current.LogOut();

            Frame.Navigate(typeof(LoginPage));
        }
    }
}
