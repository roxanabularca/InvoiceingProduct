namespace InvoiceingProduct.Models
{
    public class OfferModel
    {

        public Guid IdOffer { get; set; }
        public Guid IdProduct { get; set; }
        public Guid IdVendor { get; set; }
        public int UnitPrice { get; set; }
        public string Currency { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
