namespace Web_miniCRM.Domain.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } = (decimal)0;
        public int CountAllProduct { get; set; } = 0;
        public virtual List<Information> Informations { get; set; } = new List<Information>();
        public virtual List<InvoiceItemInfo> InvoiceItems { get; set; } = new List<InvoiceItemInfo>();
    }
}
