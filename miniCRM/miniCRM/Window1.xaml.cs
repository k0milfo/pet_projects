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
using System.Windows.Shapes;

namespace miniCRM
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window_Clients clients = Application.Current.Windows.OfType<Window_Clients>().FirstOrDefault();
            List<Client> list = new List<Client>();
                list.Add(new Client(company.Text, name.Text,
                    number_phone.Text, email.Text));
                list.AddRange(clients.clients_table.Items.Cast<Client>().ToList());
                clients.clients_table.ItemsSource = null;
                clients.clients_table.ItemsSource = list;
                this.Close();
                clients.InvalidateVisual();
        }
    }
}
