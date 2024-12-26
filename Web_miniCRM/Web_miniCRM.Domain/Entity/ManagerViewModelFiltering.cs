namespace Web_miniCRM.Domain.Entity
{
	public class ManagerViewModelFiltering
	{
		public Manager Manager { get; set; }
		public List<Invoice> FilteredInvoices { get; set; }
		public List<Call> FilteredCalls { get; set; }
		public List<Meeting> FilteredMeetings { get; set; }
	}
}
