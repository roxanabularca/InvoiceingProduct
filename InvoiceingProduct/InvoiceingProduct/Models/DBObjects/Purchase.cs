using System;
using System.Collections.Generic;

namespace InvoiceingProduct.Models.DBObjects
{
    public partial class Purchase
    {
        public Purchase()
        {
            Invoices = new HashSet<Invoice>();
        }

        public Guid IdPurchase { get; set; }
        public string PurchaseName { get; set; } = null!;
        public Guid IdOffer { get; set; }
        public int Quantity { get; set; }

        public virtual Offer IdOfferNavigation { get; set; } = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
