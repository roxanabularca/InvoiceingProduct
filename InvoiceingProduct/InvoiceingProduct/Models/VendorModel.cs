using System.ComponentModel.DataAnnotations;

namespace InvoiceingProduct.Models
{
    public class VendorModel
    {
        public Guid IdVendor { get; set; }

        [StringLength(50, ErrorMessage = "StringLength maxim 50 characters")]
        public string Name { get; set; } = null!;

        [StringLength(10, ErrorMessage = "StringLength maxim 10 characters")]
        public string DeliveryType { get; set; } = null!;

        [StringLength(50, ErrorMessage = "StringLength maxim 50 characters")]
        public string Address { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }
    }
}
