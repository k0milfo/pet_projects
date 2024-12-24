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
    public partial class AddClientWindow : Window
    {
        public bool flag { get; set; }
        DataBase dataBase = new DataBase();
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void Button_ok_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            string querystring = $"SELECT id FROM clients_db WHERE company = '{company.Text}'";
            using (SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection()))
            {
                dataBase.openConnection();
                if (command.ExecuteScalar() == null || (int)command.ExecuteScalar() == 0)
                {
                    command.CommandText = $"INSERT INTO clients_db (company, contact_person, number_phone, email) VALUES ('{company.Text}', '{name.Text}', '{number_phone.Text}', '{email.Text}')"; ;
                    command.ExecuteNonQuery();
                    Page_Clients.currentPage.initialize_ClitntsTable();
                    dataBase.closeConnection();
                }
                else
                {
                    int id = (int)command.ExecuteScalar();
                    MessageBoxResult result = MessageBoxResult.Yes;
                    if (!flag)
                    {
                        result = MessageBox.Show($"Компания {company.Text} существует, обновить данные?", "Важное сообщение)", MessageBoxButton.YesNo);
                    }

                    if (result == MessageBoxResult.Yes)
                    {
                        command.CommandText = $"UPDATE clients_db SET contact_person = '{name.Text}', number_phone = '{number_phone.Text}', email = '{email.Text}' WHERE id = {id}";
                        command.ExecuteNonQuery();
                        Page_Clients.currentPage.initialize_ClitntsTable();
                        dataBase.closeConnection();
                    }
                    if (result == MessageBoxResult.No)
                    {
                        dataBase.closeConnection();
                        this.Close();
                    }
                }
            }
            this.Close();

        }
    }
}
