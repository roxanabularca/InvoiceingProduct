using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceingProduct.Models
{
    public class OfferModel
    {
        public Guid IdOffer { get; set; }
        public Guid IdProduct { get; set; }
        public Guid IdVendor { get; set; }

        [StringLength(50, ErrorMessage = "String too long( max. 50 characters)")]
        public string OfferName { get; set; } = null!;

        [Range(0.01d, int.MaxValue, ErrorMessage = "The unit price must be a positive number.")]
        public decimal UnitPrice { get; set; }

        [StringLength(10,ErrorMessage = "String too long( max. 10 characters)")]
        public string Currency { get; set; } = null!;
        public bool IsAvailable { get; set; }

        public string? ProductName { get; set; }

        public string? VendorName { get; set; }
    }
}
