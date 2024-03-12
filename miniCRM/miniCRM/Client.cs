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
        public Client(string Company, string Name) 
        {
            this.Company = Company;
            this.Name = Name;
        }
    }
}
