namespace Web_miniCRM.Domain.Entity
{
    public class InvoiceItemInfo
    {
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Invoice? Invoice { get; set; }
        public virtual Product? Product { get; set; }
    }
}
