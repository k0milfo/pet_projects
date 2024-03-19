using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace miniCRM
{
    public partial class Page_Clients : Page
    {
        List<Client> clients_list {  get; set; }
        DataBase dataBase = new DataBase();
        public static Page_Clients currentPage { get; private set; }
        public Page_Clients()
        {
            InitializeComponent();
            initializeClitntsTable();
            Loaded += Page_Clients_Loaded;
        }

        public void initializeClitntsTable()
        {           
            string sqlQuery = "SELECT company, contact_person, number_phone, email FROM clients_db";
            using (SqlCommand command = new SqlCommand(sqlQuery, dataBase.GetConnection()))
            {
                clients_list = new List<Client>();
                dataBase.openConnection();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients_list.Add(new Client(reader.GetString(0), reader.GetString(1),
                            reader.GetString(2), reader.GetString(3)));
                    }
                    clients_table.ItemsSource = clients_list;
                    this.InvalidateVisual();
                }
                dataBase.closeConnection();
            }
        }
        public void Page_Clients_Loaded(object sender, RoutedEventArgs e)
        {
            currentPage = this;
        }

        public void EditButton_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.flag = true;
            var button = sender as Button;
            DataGridRow row = VisualTreeHelperExtensions.FindAncestor<DataGridRow>(button);
            int rowIndex = clients_table.Items.IndexOf(row.Item);
            MessageBox.Show(rowIndex.ToString());
            var intermediate = clients_list[rowIndex];
            var client = new Client(intermediate.Company, intermediate.Name, intermediate.Phone_number, intermediate.Email);

            addClientWindow.company.Text = client.Company;
            addClientWindow.name.Text = client.Name;
            addClientWindow.number_phone.Text = client.Phone_number;
            addClientWindow.email.Text = client.Email;
            addClientWindow.Title = client.Company;
            addClientWindow.Show();
        }

        public void DeleteButtin_Click(object sender, RoutedEventArgs e) 
        { 
            dataBase.openConnection();
            var button = sender as Button;
            DataGridRow row = VisualTreeHelperExtensions.FindAncestor<DataGridRow>(button);
            int rowIndex = clients_table.Items.IndexOf(row.Item);
            var client = clients_list[rowIndex];
            string querystring = $"DELETE FROM clients_db WHERE company = '{client.Company}'";
            using (SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection()))
            {
                command.ExecuteNonQuery();
                dataBase.closeConnection();
                initializeClitntsTable();
            }
        }
    }
}
