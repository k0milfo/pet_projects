
namespace Web_miniCRM.Domain.Entity
{
	public class DepartmentViewModelFiltering
	{
		public HeadDepartment HeadDepartment { get; set; }
		public List<Invoice> FilteredInvoices { get; set; }
		public List<Call> FilteredCalls { get; set; }
		public List<Meeting> FilteredMeetings { get; set; }
		public List<Company> FilteredCompanies { get; set; }
	}
}
