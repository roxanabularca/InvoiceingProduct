using System.ComponentModel.DataAnnotations;

namespace InvoiceingProduct.Models
{
    public class InvoiceModel
    {

        public Guid IdInvoice { get; set; }
        public Guid IdPurchase { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The invoice must have a number.")]
        public int InvoiceNumber { get; set; }

        [DisplayFormat(DataFormatString = "0:MM/dd/yyyy")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [Range(0.01d,int.MaxValue,ErrorMessage="The invoice amount must be a positive number.")]
        public decimal InvoiceAmount { get; set; }

        [Range(0.01d, int.MaxValue, ErrorMessage = "The tax amount must be a positive number.")]
        public decimal TaxAmount { get; set; }

        [DisplayFormat(DataFormatString = "0:MM/dd/yyyy")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public string? Comments { get; set; }

        //public List<PurchaseModel> purchaseModels { get; set; }
    }
}
