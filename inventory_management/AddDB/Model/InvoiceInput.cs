using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Model
{
    public partial class InvoiceInput
    {
        public InvoiceInput()
        {
            InvoiceInputDetails = new HashSet<InvoiceInputDetail>();
        }

        public int InputBillId { get; set; }
        public int SuplierId { get; set; }
        public int UserId { get; set; }
        public DateTime InputDate { get; set; }
        public double? Amount { get; set; }

        public virtual Suplier Suplier { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<InvoiceInputDetail> InvoiceInputDetails { get; set; }
    }
}
