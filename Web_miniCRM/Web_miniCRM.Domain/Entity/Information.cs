namespace Web_miniCRM.Domain.Entity
{
    public class Information
	{
		public int InformationId { get; set; }
		public int? InvoiceId { get; set; }
		public int? CompanyId { get; set; }
		public int? ProductId { get; set; }

		//public DateTime? DateTime { get; set; }

		public string? Details { get; set; } = string.Empty;
		public virtual Company? Company { get; set; }
		public virtual Invoice? Invoice { get; set; }
		public virtual Product? Product { get; set; }

	}
}
