using System.ComponentModel.DataAnnotations;

namespace InvoiceingProduct.Models
{
    public class ProductModel
    {
        public Guid IdProduct { get; set; }

        [StringLength(50, ErrorMessage = "String too long( max. 50 characters).")]
        public string ProductName { get; set; } = null!;

        [StringLength(250, ErrorMessage = "String too long( max. 250 characters).")]
        public string Description { get; set; } = null!;
        public string? Comments { get; set; }
    }
}
