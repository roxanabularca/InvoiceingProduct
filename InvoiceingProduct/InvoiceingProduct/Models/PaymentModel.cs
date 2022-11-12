using System.ComponentModel.DataAnnotations;

namespace InvoiceingProduct.Models
{
    public class PaymentModel
    {
        public Guid IdPayment { get; set; }
        public Guid IdInvoice { get; set; }

        [DisplayFormat(DataFormatString = "0:MM/dd/yyyy")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [StringLength(50,ErrorMessage = "String too long( max. 50 characters).")]
        public string PaymentType { get; set; } = null!;

        [Range(0.01d, int.MaxValue, ErrorMessage = "The paid amount must be a positive number.")]
        public decimal  AmountPaid { get; set; }

        [StringLength(50, ErrorMessage = "String too long( max. 50 characters)")]

        public int? InvoiceNumber { get; set; }
        public string PaymentAuthorization { get; set; } = null!;
    }
}
