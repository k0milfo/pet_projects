namespace Web_miniCRM.Domain.Entity
{
	public class Meeting
	{
		public int MeetingId { get; set; }
		public string Details { get; set; }
		public Company? Company { get; set; }
		public int? CompanyId { get; set; }
		public Manager? Manager { get; set; }
		public int ManagerId { get; set; }
		public DateTime Date { get; set; }
		public string? Address { get; set; }
		public string? NameContactPerson { get; set; }
	}
}
