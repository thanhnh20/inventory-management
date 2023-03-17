using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Model
{
    public partial class InvoiceInputDetail
    {
        public int InputDetailId { get; set; }
        public int InputBillId { get; set; }
        public int ConsignmentDetailId { get; set; }
        public int Quantity { get; set; }
        public double? TotalPrice { get; set; }

        public virtual ConsignmentDetail ConsignmentDetail { get; set; }
        public virtual InvoiceInput InputBill { get; set; }
    }
}
