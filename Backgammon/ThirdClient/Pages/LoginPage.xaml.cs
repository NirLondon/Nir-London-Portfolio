using Backgammon.ViewModels;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using System;

namespace Client2.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; set; }
        public LoginPage()
        {
            ViewModel = new LoginViewModel();
            ViewModel.Signedin += () => Frame.Navigate(typeof(OpeningPage));
            ViewModel.FailedToSignin += () => OnLoginOrSigninFailed(false);
            ViewModel.Logedin += () => Frame.Navigate(typeof(OpeningPage));
            ViewModel.FailedToLogin += () => OnLoginOrSigninFailed();

            this.InitializeComponent();
        }

        void OnLoginOrSigninFailed(bool isLogin = true)
        {
            var msg = new MessageDialog("Failed to" + (isLogin ? "log in" : "sign in") + "...\nPlease try again.");
            msg.Commands.Add(new UICommand("OK", uic => { }));
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await msg.ShowAsync());
        }
    }
}
