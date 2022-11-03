namespace InvoiceingProduct.Models
{
    public class PaymentModel
    {
        public Guid IdPayment { get; set; }
        public Guid IdInvoice { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; } = null!;
        public int AmountPaid { get; set; }
        public string PaymentAuthorization { get; set; } = null!;
    }
}
