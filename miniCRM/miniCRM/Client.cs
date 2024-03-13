using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniCRM
{
    internal class Client
    {
        public string Company { get; set; }
        public string Name { get; set; }
        public string Phone_number { get; set; }
        public string Email { get; set; }
        public Client(string Company, string Name, string Phone_number, string Email) 
        {
            this.Company = Company;
            this.Name = Name;
            this.Phone_number = Phone_number;
            this.Email = Email;
        }
    }
}
