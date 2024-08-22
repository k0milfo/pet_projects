namespace Web_miniCRM.Domain.Entity
{
	public class Manager
	{
		public int ManagerId { get; set; }
		public int DepartmentNumber { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string NumberPhone { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public virtual List<Call>? Calls { get; set; } = new List<Call>();
		public virtual List<Company>? Companies { get; set; } = new List<Company>();
		public virtual List<Meeting>? Meetings { get; set; } = new List<Meeting>();
		public virtual List<Invoice>? Invoices { get; set; } = new List<Invoice>();
	}
}
