using System.ComponentModel.DataAnnotations;

namespace InvoiceingProduct.Models
{
    public class PurchaseModel
    {
        public Guid IdPurchase { get; set; }
        public Guid IdOffer { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be a positive number.")]
        public int Quantity { get; set; }

        //public List<OfferModel> offerModels { get; set; }
    }
}
