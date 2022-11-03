﻿using System;
using System.Collections.Generic;

namespace InvoiceingProduct.Models.DBObjects
{
    public partial class Invoice
    {
        public Invoice()
        {
            Payments = new HashSet<Payment>();
        }

        public Guid IdInvoice { get; set; }
        public Guid IdPurchase { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int InvoiceAmount { get; set; }
        public int TaxAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string? Comments { get; set; }

        public virtual Purchase IdPurchaseNavigation { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}