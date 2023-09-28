using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net; 
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Watsap.ViewModel.Helpers;

namespace Watsap.ViewModel
{
    internal class MainWindowViewModel: INotifyPropertyChanged
    {
        #region
        public BindableCommand AddCommand { get; set; }
        #endregion
        public NewChatWindowViewModel _newChat;

        private string _userName;
        public string userName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }

        private string _userIp;
        public string userIp
        {
            get { return _userIp; }
            set { _userIp = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            AddCommand = new BindableCommand(_ =>
            {
                Validation();
            });
        }
        private static bool ValidateIpAddress(string ipAddress)
        {
            IPAddress address;
            bool isValidIpAddress = IPAddress.TryParse(ipAddress, out address);

            return isValidIpAddress;
        }
        private static bool ValidateUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9_\-]+$";
            bool isValidUsername = Regex.IsMatch(username, pattern);

            return isValidUsername;
        }
        private void Validation()
        {
            if (ValidateIpAddress(userIp) == true && ValidateUsername(userName) == true)
            {
                _newChat = new NewChatWindowViewModel(_userName, _userIp);
                _newChat.OpenNewChatWindow();
            }
            else
            {
                MessageBox.Show("Неправильно введено ip или имя пользователя!");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
