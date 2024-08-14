namespace Web_miniCRM.Domain.Entity
{
    public class CreateInvoiceViewModel
    {
        public CreateInvoiceViewModel(IEnumerable<Company>? companies, IEnumerable<Product>? products, Information? information, IEnumerable<Manager>? managers)
        {
            _companies = companies;
            _products = products;
            _information = information;
            _managers = managers;

		}
		public IEnumerable<Manager>? _managers { get; set; }
		public IEnumerable<Company>? _companies { get; set; }
        public IEnumerable<Product>? _products { get; set; }
        public Information? _information { get; set; }
    }
}
