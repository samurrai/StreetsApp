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

namespace StreetsAppClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            indexesComboBox.Items.Add("010000");
            indexesComboBox.Items.Add("010003");
            indexesComboBox.Items.Add("010004");
            indexesComboBox.Items.Add("010009");
            indexesComboBox.Items.Add("010010");
        }

        private void GetStreetsListButtonClick(object sender, RoutedEventArgs e)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345));
            socket.Send(Encoding.UTF8.GetBytes(indexesComboBox.SelectedItem as string));
            byte[] buffer = new byte[1024 * 4];
            socket.Receive(buffer);
            MessageBox.Show(Encoding.UTF8.GetString(buffer));
        }
    }
}
