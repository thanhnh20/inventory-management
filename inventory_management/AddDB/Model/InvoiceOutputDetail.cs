using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Model
{
    public partial class InvoiceOutputDetail
    {
        public int OutputDetailId { get; set; }
        public int OutputBillId { get; set; }
        public int ConsignmentDetailId { get; set; }
        public int Quantity { get; set; }
        public double? TotalPrice { get; set; }

        public virtual ConsignmentDetail ConsignmentDetail { get; set; }
        public virtual InvoiceOutput OutputBill { get; set; }
    }
}
