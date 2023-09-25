using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Watsap
{
   
    internal class TcpClient
    {
        public ListBox messages;
        public ListBox userBox;
        public string Name;
        public CancellationTokenSource _IsWorking;
        public Socket server;

        public TcpClient(string name,Socket server, string ip,ListBox box, ListBox userBox)
        {
            messages = box;
            this.userBox = userBox;
            Name = name;
            this.server = server;
            server.Connect(ip, 8888);
            _IsWorking = new CancellationTokenSource();
            

        }
        public async Task ReceiveMessage(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                byte[] bytes = new byte[1024];
                await server.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                string username;
                if (message.Contains("/connect_username"))
                {
                    username = message.Substring(message.LastIndexOf("/connect_username"));
                    username = username.Remove(0, 19);
                    message = message.Remove(message.LastIndexOf("/connect_username"));
                    userBox.Items.Add($"[{username}]");
                } 
                if (message.Contains("/exit") || message == ("/disconnect"))
                {
                    
                    _IsWorking.Cancel();
                }
            
                messages.Items.Add(message);
            }
        }
        public async Task SendMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await server.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }
        public void Dispose()
        {
            _IsWorking.Cancel();
            SendMessage(null).Dispose();
            ReceiveMessage(_IsWorking.Token).Dispose();
        }
    }

}
