namespace Web_miniCRM.Domain.Entity
{
    public class CreateInvoiceViewModel
    {
  //      public CreateInvoiceViewModel(IEnumerable<Company>? companies, IEnumerable<Product>? products, Information? information, IEnumerable<Manager>? managers)
  //      {
  //          _companies = companies;
  //          _products = products;
  //          _information = information;
  //          _managers = managers;

		//}
		//public List<Manager>? _managers { get; set; }
		//public List<Company>? _companies { get; set; }
        public List<Product>? _products { get; set; }
        public Information? _information { get; set; }
        public Manager? _manager { get; set; }
    }
}
