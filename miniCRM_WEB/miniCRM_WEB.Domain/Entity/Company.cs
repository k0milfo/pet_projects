using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniCRM_WEB.Domain.Entity
{
	public class Company
	{
		public int CompanyId { get; set; }
		public string NameContactPerson { get; set; }
		public string CompanyName { get; set; }
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; }
		public string INN { get; set; }
		public virtual List<Information> Informations { get; set; } = new List<Information>();
		public virtual List<Invoice> Invoices { get; set; } = new List<Invoice>();
	}
	public class Invoice
	{
		public int InvoiceId { get; set; }
		public Nullable<DateTime> InvoiceDate { get; set; }
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		public virtual List<Information> Informations { get; set; } = new List<Information>();
		public virtual List<InvoiceItemInfo> InvoiceItems { get; set; } = new List<InvoiceItemInfo>();
	}

	public class Product
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; } = 0;
		public int CountAllProduct { get; set; } = 0;
		public virtual List<Information> Informations { get; set; } = new List<Information>();
		public virtual List<InvoiceItemInfo> InvoiceItems { get; set; } = new List<InvoiceItemInfo>();
	}
	public class Information
	{
		public int InformationId { get; set; }
		public Nullable<int> InvoiceId { get; set; }
		public Nullable<int> CompanyId { get; set; }
		public Nullable<int> ProductId { get; set; }

		public string Details { get; set; }
		public virtual Company Company { get; set; }
		public virtual Invoice Invoice { get; set; }
		public virtual Product Product { get; set; }

	}
	public class InvoiceItemInfo
	{
		public int InvoiceItemId { get; set; }
		public int InvoiceId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }

		public virtual Invoice Invoice { get; set; }
		public virtual Product Product { get; set; }
	}
}
