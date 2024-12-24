using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_miniCRM.Domain.Entity
{
	public class CompanyViewModelFiltering
	{
		public Company Company { get; set; }
		public List<Invoice> FilteredInvoices { get; set; }
		public List<Call> FilteredCalls { get; set; }
		public List<Meeting> FilteredMeetings { get; set; }
	}
}
