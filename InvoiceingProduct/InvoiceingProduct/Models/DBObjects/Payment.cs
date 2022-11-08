using System;
using System.Collections.Generic;

namespace InvoiceingProduct.Models.DBObjects
{
    public partial class Payment
    {
        public Guid IdPayment { get; set; }
        public Guid IdInvoice { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; } = null!;
        public decimal AmountPaid { get; set; }
        public string PaymentAuthorization { get; set; } = null!;

        public virtual Invoice IdInvoiceNavigation { get; set; } = null!;
    }
}
