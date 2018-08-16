using Backgammon.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Client2.Pages
{
    public sealed partial class OpeningPage : Page
    {
        ContactingViewModel viewModel { get; set; }
        public OpeningPage()
        {
            viewModel = new ContactingViewModel();
            viewModel.AskedToPlay += asking =>
            {
                TaskCompletionSource<bool> res = new TaskCompletionSource<bool>();

                var msg = new MessageDialog(asking + "asks to play with you.");
                msg.Commands.Add(new UICommand("Accept", uiCom => res.SetResult(true)));
                msg.Commands.Add(new UICommand("Refuse", uiCom => res.SetResult(false)));
                msg.DefaultCommandIndex = 1;
                msg.CancelCommandIndex = 1;

                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await msg.ShowAsync()).AsTask().Wait();

                return res.Task.Result;
            };
            viewModel.GameStarting += async (starting, turn) =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                Frame.Navigate(typeof(MainPage), (starting, turn/*dice)*/)));
            };
            viewModel.RequestRefused += refuser =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    var msg = new MessageDialog(refuser + " refused toplay with you... :(");
                    msg.Commands.Add(new UICommand("OK", uic => { }));
                    await msg.ShowAsync();
                });
            };
            this.InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.AskToPlay((((sender as ListView).SelectedItem as Tuple<string, string>)).Item1);
        }
    }
}
