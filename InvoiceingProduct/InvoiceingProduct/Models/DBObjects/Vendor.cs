using System;
using System.Collections.Generic;

namespace InvoiceingProduct.Models.DBObjects
{
    public partial class Vendor
    {
        public Vendor()
        {
            Offers = new HashSet<Offer>();
        }

        public Guid IdVendor { get; set; }
        public string Name { get; set; } = null!;
        public string DeliveryType { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Email { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
