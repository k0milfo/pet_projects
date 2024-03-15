using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            initializeClitntsTable();
        }
        // Создать метод для обновления таблицы
        public void initializeClitntsTable()
        {
            DataBase dataBase = new DataBase();
            string sqlQuery = "SELECT company, contact_person, number_phone, email FROM clients_db";
            using (SqlCommand command = new SqlCommand(sqlQuery, dataBase.GetConnection()))
            {
                List<Client> list = new List<Client>();
                dataBase.openConnection();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Client(reader.GetString(0), reader.GetString(1), 
                            reader.GetString(2), reader.GetString(3)));
                    }
                    clients_table.ItemsSource = list;
                    this.InvalidateVisual();
                }
                dataBase.closeConnection();
            }
        }

        private void add_client_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow window1 = new AddClientWindow();
            window1.Show();
            
        }
        private void back_to_mainMenu(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row != null && row.Item is string)
            {
                string item = row.Item as string;

            }
        }
    }
}
