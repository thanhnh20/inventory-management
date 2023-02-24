using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Consignment
    {
        public Consignment()
        {
            InvoiceInputDetails = new HashSet<InvoiceInputDetail>();
            InvoiceOutputDetails = new HashSet<InvoiceOutputDetail>();
            ProductConsignments = new HashSet<ProductConsignment>();
            Products = new HashSet<Product>();
        }

        public int ConsignmentId { get; set; }
        public string ConsignmentName { get; set; }
        public int Status { get; set; }

        public virtual ICollection<InvoiceInputDetail> InvoiceInputDetails { get; set; }
        public virtual ICollection<InvoiceOutputDetail> InvoiceOutputDetails { get; set; }
        public virtual ICollection<ProductConsignment> ProductConsignments { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
