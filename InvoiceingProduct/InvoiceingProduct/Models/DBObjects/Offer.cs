using System;
using System.Collections.Generic;

namespace InvoiceingProduct.Models.DBObjects
{
    public partial class Offer
    {
        public Offer()
        {
            Purchases = new HashSet<Purchase>();
        }

        public Guid IdOffer { get; set; }
        public Guid IdProduct { get; set; }
        public Guid IdVendor { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = null!;
        public bool IsAvailable { get; set; }

        public virtual Product IdProductNavigation { get; set; } = null!;
        public virtual Vendor IdVendorNavigation { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
