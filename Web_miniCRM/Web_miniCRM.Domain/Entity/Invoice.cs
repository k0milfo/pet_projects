using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace Web_miniCRM.Domain.Entity
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public bool ShippedInvoiced { get; set; } = false;
		public int CompanyId { get; set; }
        //[BindNever] // предотвращения привязки данных из запросов к указанному свойству модели
        public Company? Company { get; set; }
        public virtual List<Information>? Informations { get; set; } = new List<Information>();
        public virtual List<InvoiceItemInfo> InvoiceItems { get; set; } = new List<InvoiceItemInfo>();
    }
}
