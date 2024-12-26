using System;
using System.Collections.Generic;
using System.Linq;
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

namespace miniCRM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            add_clients1.Visibility = Visibility.Hidden;
            add_phone_call.Visibility = Visibility.Hidden;
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            add_phone_call.Visibility = Visibility.Hidden;
            mainFrame.Navigate(new Page_Clients());
            add_clients1.Visibility = Visibility.Visible;
        }

        private void add_clients1_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow add = new AddClientWindow();
            add.Show();
        }

        private void add_phone_call_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
