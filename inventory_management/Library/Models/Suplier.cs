using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Suplier
    {
        public Suplier()
        {
            InvoiceInputs = new HashSet<InvoiceInput>();
        }

        public int SuplierId { get; set; }
        public string SuplierName { get; set; }
        public string SuplierPhone { get; set; }
        public string TaxCode { get; set; }
        public int Status { get; set; }

        public virtual ICollection<InvoiceInput> InvoiceInputs { get; set; }
    }
}
