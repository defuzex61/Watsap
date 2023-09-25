using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Watsap
{
    /// <summary> 
    /// Логика взаимодействия для MainWindow.xaml 
    /// </summary> 
    internal class TcpServer
    {
        private Socket _socket;
        private ListBox _messageBox,_userBox;
        private List<Socket> _clients = new List<Socket>();
        public async Task StartServerAsync( Socket socket, ListBox box, ListBox userBox)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8888);
            _socket = socket;
            _userBox = userBox;
            _messageBox = box;
            
            _socket.Bind(endPoint);
            _socket.Listen(100);
            await ListenToClients();

        }
        private async Task ListenToClients()
        {
            while (true)
            {
                var client = await _socket.AcceptAsync();
                _clients.Add(client);
                ReceiveMessage(client);
            }
        }
        private async Task ReceiveMessage(Socket client)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await client.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                string username = "";
                
                if (message.Contains("/connect_user"))
                {
                    string messageWithTag = message;
                    username = message.Substring(message.LastIndexOf("/connect_user"));
                    username = username.Remove(0, 19);
                    message = message.Remove(message.LastIndexOf("/connect_user"));
                    _userBox.Items.Add($"[{username}]");
                    message = messageWithTag;
                }
                
                _messageBox.Items.Add($"Sended:{DateTime.Now.ToString("HH:mm:ss")}\tsenderIP: {client.RemoteEndPoint} \nmessage sended to clients:\n {message}");
                foreach (var item in _clients)
                {
                    SendMessage(item, message);
                }

            }
        } 
        private async Task SendMessage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }
    }
}