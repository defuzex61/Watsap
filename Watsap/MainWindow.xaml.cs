using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class MainWindow : Window
    {
        private NewChatWindow _newChat;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newChatBtn_Click(object sender, RoutedEventArgs e)
        {
            validation();  
        }

        private void oldChatBtn_Click(object sender, RoutedEventArgs e)
        {
            validation();
        }
        private void validation()
        {
            Regex ipCheck = new Regex("\\b(?:\\d{1,3}\\.){3}\\d{1,3}\\b\r\n");
            Regex loginCheck = new Regex("^[a-zA-Z0-9_-]{3,16}$\r\n");

            if (Regex.IsMatch(userNameTb.Text, loginCheck.ToString()) && Regex.IsMatch(ipTb.Text, ipCheck.ToString()))
            {
                _newChat = new NewChatWindow(ipTb.Text, userNameTb.Text);
                _newChat.Show();
            }
            else
            {
                MessageBox.Show("Неправильно введено ip или имя пользователя!");
            }
        }
    }
}
 