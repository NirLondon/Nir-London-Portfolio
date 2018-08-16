using Backgammon.Common.ChatLogic;
using Backgammon.ViewModels;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Client2.UserControlls
{
    public sealed partial class ChatUC : UserControl
    {
        public ChatViewModel viewModel { get; set; }
        ObservableCollection<Message> conversation;
        public ChatUC()
        {
            conversation = new ObservableCollection<Message>();
            viewModel = new ChatViewModel();
            viewModel.MessageReceived += m => Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => conversation.Add(m));
            this.InitializeComponent();
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            viewModel.SendMessage(tbxMessageEdit.Text);
        }
    }
}
