using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
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
    public partial class AddClientWindow : Window
    {
        DataBase dataBase = new DataBase();
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void Button_ok_Click(object sender, RoutedEventArgs e)
        {
            Window_Clients clients = Application.Current.Windows.OfType<Window_Clients>().FirstOrDefault();
            string querystring = $"INSERT INTO clients_db (company, contact_person, number_phone, email) VALUES ('{company.Text}', '{name.Text}', '{number_phone.Text}', '{email.Text}')";
            using (SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection()))
            {
                dataBase.openConnection();
                command.ExecuteNonQuery();
                clients.initializeClitntsTable();
                dataBase.closeConnection();
            }
            this.Close();

        }
    }
}
