namespace Web_miniCRM.Domain.Entity
{
	public class Call
	{
		public int CallId { get; set; }
		public int ManagerId { get; set; }
		public int? CompanyId { get; set; }
		public string? Details { get; set; }
		public Company? Company { get; set; }
		public Manager? Manager { get; set; }
		public DateTime Date {  get; set; }
		public string PhoneNumber { get; set; }
		public string? NameContactPerson { get; set; }
	}
}
