using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Model
{
    public partial class InvoiceOutput
    {
        public InvoiceOutput()
        {
            InvoiceOutputDetails = new HashSet<InvoiceOutputDetail>();
        }

        public int OutputBillId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public DateTime OutputDate { get; set; }
        public double? Amount { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<InvoiceOutputDetail> InvoiceOutputDetails { get; set; }
    }
}
