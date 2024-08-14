namespace Web_miniCRM.Domain.Entity
{
	public class ManagerViewModel
	{
		public Manager Manager { get; set; }
		public IEnumerable<Invoice> FilteredInvoices { get; set; }
	}
}
