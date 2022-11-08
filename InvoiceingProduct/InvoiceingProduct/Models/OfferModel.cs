using System.ComponentModel.DataAnnotations;

namespace InvoiceingProduct.Models
{
    public class OfferModel
    {

        public Guid IdOffer { get; set; }
        public Guid IdProduct { get; set; }
        public Guid IdVendor { get; set; }

        [Range(0.01d, int.MaxValue, ErrorMessage = "The unit price must be a positive number.")]
        public decimal UnitPrice { get; set; }

        [StringLength(10,ErrorMessage ="StringLength maxim 10 characters")]
        public string Currency { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
