namespace Web_miniCRM.Domain.Entity
{
	public class Meeting
	{
		public int MeetingId { get; set; }
		public string Details { get; set; } = string.Empty;
		public Company? Company { get; set; }
		public int? CompanyId { get; set; }
		public Manager Manager { get; set; } = new Manager();
		public int ManagerId { get; set; }
		public DateTime Date { get; set; }
		public string Address { get; set; } = string.Empty;
		public string? NameContactPerson { get; set; }
	}
}
