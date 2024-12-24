using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniCRM
{
    internal class Communications
    {
        public int CommunicationID { get; set; }
        public int ClientID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Name_Communication { get; set; }

        public Communications(string Name_Communication, DateTime Date, string Description)
        {
            this.Name_Communication = Name_Communication;
            this.Date = Date;
            this.Description = Description;
        }
    }
}
