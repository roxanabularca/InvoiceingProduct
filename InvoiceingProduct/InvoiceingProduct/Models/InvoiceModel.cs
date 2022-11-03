namespace InvoiceingProduct.Models
{
    public class InvoiceModel
    {

        public Guid IdInvoice { get; set; }
        public Guid IdPurchase { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int InvoiceAmount { get; set; }
        public int TaxAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string? Comments { get; set; }
    }
}
