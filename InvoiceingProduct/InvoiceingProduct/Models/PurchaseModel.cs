using System.ComponentModel.DataAnnotations;

namespace InvoiceingProduct.Models
{
    public class PurchaseModel
    {
        public Guid IdPurchase { get; set; }
        public Guid IdOffer { get; set; }

        [StringLength(50, ErrorMessage = "String too long( max. 50 characters)")]
        public string PurchaseName { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be a positive number.")]
        public int Quantity { get; set; }

        public string? OfferName { get; set; }

        //public List<OfferModel> offerModels { get; set; }
    }
}
