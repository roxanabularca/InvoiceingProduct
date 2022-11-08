using System;
using System.Collections.Generic;

namespace InvoiceingProduct.Models.DBObjects
{
    public partial class Product
    {
        public Product()
        {
            Offers = new HashSet<Offer>();
        }

        public Guid IdProduct { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Comments { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
