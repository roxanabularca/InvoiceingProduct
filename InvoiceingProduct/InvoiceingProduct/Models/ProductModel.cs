using System.ComponentModel.DataAnnotations;

namespace InvoiceingProduct.Models
{
    public class ProductModel
    {
        public Guid IdProduct { get; set; }

        [StringLength(50, ErrorMessage = "StringLength maxim 50 characters")]
        public string Description { get; set; } = null!;
        public string? Comments { get; set; }
    }
}
