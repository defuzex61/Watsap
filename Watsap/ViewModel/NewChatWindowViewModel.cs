using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Watsap.ViewModel.Helpers;

namespace Watsap.ViewModel
{
    internal class NewChatWindowViewModel : INotifyPropertyChanged
    {
        private TcpClient _tcpClient;
        private TcpServer _tcpServer;

        #region
        public BindableCommand sendMsgBtn { get; set; }
        public BindableCommand exitMsgBtn { get; set; }
        #endregion

        private ObservableCollection<string> _messagesLbx =new ObservableCollection<string>();
        public ObservableCollection<string> messagesLbx
        {
            get { return _messagesLbx; }
            set { _messagesLbx = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _usersLbx = new ObservableCollection<string>();
        public ObservableCollection<string> usersLbx
        {
            get { return _usersLbx; }
            set { _usersLbx = value; OnPropertyChanged(); }
        }

        private string _messageTb;
        public string messageTb
        {
            get { return _messageTb; }
            set { _messageTb = value; OnPropertyChanged(); }
        }
        public ListBox meslistBox = new ListBox();
        public ListBox userlistBox = new ListBox();

        public NewChatWindowViewModel() {
            sendMsgBtn = new BindableCommand(_ => { sendBtn_Click(); });
            exitMsgBtn = new BindableCommand(_ => { exitBtn_Click(); });
        }
        public NewChatWindowViewModel(string name, string ip):this() 
        {

            
            meslistBox.ItemsSource = messagesLbx;
            userlistBox.ItemsSource = usersLbx;
            var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _tcpServer = new TcpServer();
            var serverTask = _tcpServer.StartServerAsync(serverSocket, meslistBox, userlistBox);
            var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _tcpClient = new TcpClient(name, clientSocket, ip, meslistBox, userlistBox);
            _tcpClient.ReceiveMessage(_tcpClient.IsWorking.Token);
            _tcpClient.SendMessage($"{name} подключился... /connect_username= {name}");

        }


        private void sendBtn_Click()
        {
            sendMsg(messageTb);
            Thread.Sleep(5);
        }
        private async Task sendMsg(string message)
        {
            await _tcpClient.SendMessage(message);
        }

        private void exitBtn_Click()
        {
            _tcpClient.Dispose();
            NewChatWindow newChat = new NewChatWindow();
            newChat.Close();
        }
        public void OpenNewChatWindow()
        {
            NewChatWindow newChat = new NewChatWindow();
            newChat.Owner = Application.Current.MainWindow;
            newChat.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            newChat.Show();
            meslistBox.ItemsSource = messagesLbx;
            userlistBox.ItemsSource = usersLbx;


        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
    }
}

