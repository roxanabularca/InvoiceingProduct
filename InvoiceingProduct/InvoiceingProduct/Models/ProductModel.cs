namespace InvoiceingProduct.Models
{
    public class ProductModel
    {
        public Guid IdProduct { get; set; }
        public string Description { get; set; } = null!;
        public string? Comments { get; set; }
    }
}
