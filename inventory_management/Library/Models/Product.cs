using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Product
    {
        public Product()
        {
            InvoiceInputDetails = new HashSet<InvoiceInputDetail>();
            InvoiceOutputDetails = new HashSet<InvoiceOutputDetail>();
            ProductConsignments = new HashSet<ProductConsignment>();
        }

        public int ProductId { get; set; }
        public int ConsignmentId { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public int? TotalQuantity { get; set; }
        public int? Status { get; set; }
        public double? ImportPrice { get; set; }
        public double? SellingPrice { get; set; }

        public virtual Consignment Consignment { get; set; }
        public virtual ICollection<InvoiceInputDetail> InvoiceInputDetails { get; set; }
        public virtual ICollection<InvoiceOutputDetail> InvoiceOutputDetails { get; set; }
        public virtual ICollection<ProductConsignment> ProductConsignments { get; set; }
    }
}
