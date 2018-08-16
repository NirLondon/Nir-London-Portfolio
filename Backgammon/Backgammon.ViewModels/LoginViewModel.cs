using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                Notify(nameof(UserName));
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                Notify(nameof(Password));
            }
        }

        public async void Login()
        {
            if (await Connection.Current.AuthenticationHubProxy.Invoke<bool>("Login", userName, password))
            {
                Connection.Current.Status.UserName = userName;
                Logedin?.Invoke();
            }
            else FailedToLogin?.Invoke();
        }

        public async void Signin()
        {
            if (await Connection.Current.AuthenticationHubProxy.Invoke<bool>("Signin", userName, password))
            {
                Connection.Current.Status.UserName = userName;
                Signedin?.Invoke();
            }
            else FailedToSignin?.Invoke();
        }

        public event Action Logedin;
        public event Action FailedToLogin;

        public event Action Signedin;
        public event Action FailedToSignin;

        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
