namespace InvoiceingProduct.Models
{
    public class VendorModel
    {
        public Guid IdVendor { get; set; }
        public string DeliveryType { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Email { get; set; }
    }
}
