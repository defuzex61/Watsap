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
        public CancellationTokenSource IsWorking;
        public Socket server;

        public TcpClient(string name,Socket server, string ip,ListBox box, ListBox userBox)
        {
            messages = box;
            this.userBox = userBox;
            Name = name;
            this.server = server;
            server.Connect(ip, 8888);
            IsWorking = new CancellationTokenSource();
            

        }
        public async Task ReceiveMessage(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                byte[] bytes = new byte[1024];
                await server.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                string user = "";
                if (message.Contains(" /connect_user"))
                {
                    user = message.Substring(message.LastIndexOf("/connect_user"));
                    user = user.Remove(0, 19);
                    message = message.Remove(message.LastIndexOf("/connect_user"));
                    userBox.Items.Add($"[{user}]");
                } 
                if (message.Contains("/exit") || message == ("/disconnect"))
                {
                    await SendMessage($"{user} disconnected");
                    Dispose();
                }
            
                messages.Items.Add($"{ DateTime.Now.ToString("HH:mm:ss")}: {message}");

            }
        }
        public async Task SendMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await server.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }
        public void Dispose()
        {
            IsWorking.Cancel();
            SendMessage(null).Dispose();
            ReceiveMessage(IsWorking.Token).Dispose();
        }
    }

}
