namespace InvoiceingProduct.Models
{
    public class PurchaseModel
    {
        public Guid IdPurchase { get; set; }
        public Guid IdOffer { get; set; }
        public int Quantity { get; set; }

        
    }
}
