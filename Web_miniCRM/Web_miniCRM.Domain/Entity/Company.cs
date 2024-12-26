namespace Web_miniCRM.Domain.Entity
{
    public class Company
	{
		public int CompanyId { get; set; }
		public bool MainCompany {  get; set; } = false;
		public string NameContactPerson { get; set; }
        public string CompanyName { get; set; } 
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
		public string INN { get; set; }
		public Manager? Manager { get; set; }
		public int? ManagerId { get; set; }
		public virtual List<Information>? Informations { get; set; } = new List<Information>();
		public virtual List<Invoice>? Invoices { get; set; } = new List<Invoice>();
		public virtual List<Meeting>? Meetings { get; set; } = new List<Meeting>();
		public virtual List<Call>? Calls { get; set; } = new List<Call>();

	}
}
