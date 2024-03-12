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
    public partial class Window_Clients : Window
    {
        public Window_Clients()
        {
            InitializeComponent();
            List<Client> clients = new List<Client>()
            {
                new Client("Рога и копыта", "Александр"),
                new Client("2 гуся", "Евгений"),
                new Client("Смалец", "Сергей")
            };

            test_table.ItemsSource = clients;
        }
    }
}
